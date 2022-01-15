using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public MonsterStat stat;

    const float MOVE_DELAY = 0.5f;
    const float STIFFNESS_TIME = 1f;

    const float KNOCKBACK_SPEED = 0.5f;

    Transform playerTransform;
    SpriteRenderer spriteRenderer;

    public float maxChaseDistance;
    public float minChaseDistance;
    public float defaultMoveSpeed;
    private float moveSpeed;

    Vector3 dirVec;

    bool isMove;
    bool isStiffened;
    bool isDead;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            Move();
        }
        else
        {
            Dead();
        }
    }

    void Move()
    {
        if (moveSpeed > 0)
        {
            transform.Translate(dirVec * Time.deltaTime * moveSpeed);
            moveSpeed -= Time.deltaTime * moveSpeed * 4;
        }
        if (Vector2.Distance(transform.position, playerTransform.position) < maxChaseDistance && Vector2.Distance(transform.position, playerTransform.position) > minChaseDistance)
        {
            if (!isMove && !isStiffened)
            {
                LookAtPlayer();
                moveSpeed = defaultMoveSpeed;
                isMove = true;
                Invoke("OffMove", MOVE_DELAY);
            }
        }
    }

    void LookAtPlayer()
    {
        dirVec = (playerTransform.position - transform.position).normalized;
    }

    private void OffMove()
    {
        isMove = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
            return;
        if (collision.tag == "PlayerAttack")
        {
            OnDamage(collision.transform);
        }
    }
    void OnDamage(Transform attack)
    {
        CancelInvoke("Stiffen");

        dirVec = new Vector2(transform.position.x - attack.position.x, 0).normalized;
        moveSpeed = KNOCKBACK_SPEED;
        isStiffened = true;

        stat.hp -= stat.attack;
        Debug.Log(stat.objectName + "'s HP : " + stat.hp);

        if(stat.hp <= 0)
        {
            isDead = true;
            Debug.Log(stat.objectName + " is dead.");
            return;
        }

        Invoke("Stiffen", STIFFNESS_TIME);
    }

    private void Stiffen()
    {
        isStiffened = false;
    }

    private void Dead()
    {
        Color color = spriteRenderer.color;
        color.a -= Time.deltaTime;
        spriteRenderer.color = color;

        if(spriteRenderer.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
