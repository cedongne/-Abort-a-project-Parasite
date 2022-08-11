using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    const int TWO_TIMES = 2;
    const int THREE_TIMES = 3;

    const float RUN_KEY_INPUT_INTERVAL = 0.2f;
    const float HALF = 0.5f;

    const float COMBO_INTERVAL = 1.2f;
    const int MAX_COMBO_COUNT = 3;

    const int NONE = 0;
    
	public static PlayerStat playerStat;

    List<IDropItem> DropItem;

    public float moveSpeed;
    public float jumpSpeed;
    public float defaultAttackMoveSpeed;
    float attackMoveSpeed;

    float curSpeed;
    Vector3 curDirection;

    public GameObject player;
    Transform playerTransform;
    GameObject playerHitBox;
    Transform playerHitBoxTransform;

    Animator animator;

    float hAxis;
    float vAxis;

    KeyCode arrowInput;
    float runTimer;

    bool isWalk;
    bool isJump;
    bool isAttack;

    float prevhAxis = 1;

    bool interactionDown;
    bool jumpDown;
    bool attackDown;

    bool nextCombo;
    float attackDelay;

    int attackCount;

    private void Awake()
    {
		WorldObjectType = Define.WorldObject.Player;
        DropItem = new List<IDropItem>();

        playerTransform = player.transform;
        playerHitBox = GameObject.Find("PlayerHitBox");
        playerHitBoxTransform = playerHitBox.transform;

        animator = GetComponentInChildren<Animator>();

        curDirection = playerTransform.localScale;
        //		if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
        //			Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    private void Start()
    {
        playerHitBox.SetActive(false);

        moveSpeed = playerStat.moveSpeed;
        StartCoroutine("GainItem");
    }

    private void FixedUpdate()
    {
        if (!isAttack)
        {
            Move();
        }
        else
        {
            AttackMove();
        }
    }

    private void Update()
    {
        if (!ActionCheck())
        {
            GetActionInput();
        }

        GetInput();
        Run();
        Attack();
        Jump();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }

    void GetActionInput()
    {
        interactionDown = Input.GetButtonDown("Interaction");
        jumpDown = Input.GetButtonDown("Jump");
        attackDown = Input.GetButtonDown("Attack");
    }

    bool ActionCheck()
    {
        jumpDown = false;
        attackDown = false;
        interactionDown = false;
        return isAttack || isJump;
    }

    void StopAllAnimation()
    {
        animator.SetBool("isWalk", false);
    }

    void Move()
    {
        FlipForMove();
        animator.SetBool("isWalk", isWalk);
        Vector2 dirVec = new Vector2(hAxis, vAxis * HALF);
        if (dirVec.Equals(Vector2.zero))
            isWalk = false;
        else
            isWalk = true;
        transform.Translate(dirVec * Time.deltaTime * moveSpeed);
    }

    void AttackMove()
    {
        if (attackMoveSpeed > 0)
        {
            Vector2 dirVec = new Vector2(hAxis, 0);
            if (dirVec.x * playerTransform.localScale.x > 0)
                transform.Translate(dirVec * Time.deltaTime * attackMoveSpeed);
            attackMoveSpeed -= Time.deltaTime * defaultAttackMoveSpeed * THREE_TIMES;
        }
    }

    void Run()
    {
        if (arrowInput == NONE)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                arrowInput = KeyCode.RightArrow;
                runTimer = 0;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                arrowInput = KeyCode.LeftArrow;
                runTimer = 0;
            }
        }
        else
        {
            runTimer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(arrowInput == KeyCode.RightArrow && runTimer < RUN_KEY_INPUT_INTERVAL)
                {
//                    animator.SetBool("isRun", true);
                    moveSpeed *= TWO_TIMES;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (arrowInput == KeyCode.LeftArrow && runTimer < RUN_KEY_INPUT_INTERVAL)
                {
//                    animator.SetBool("isRun", true);
                    moveSpeed *= TWO_TIMES;
                }
            }
        }
        if(runTimer > RUN_KEY_INPUT_INTERVAL)
        {
            runTimer = 0;
            arrowInput = NONE;
        }
        if (moveSpeed == playerStat.moveSpeed * TWO_TIMES && hAxis == 0 && vAxis == 0)
        {
//            animator.SetBool("isRun", false);
            moveSpeed *= HALF;
        }
    }

    void FlipForMove()
    {
        if (!hAxis.Equals(0) && !prevhAxis.Equals(hAxis))
        {
            playerTransform.localScale = new Vector3(playerTransform.localScale.x * (-1), playerTransform.localScale.y, playerTransform.localScale.z);
            playerHitBoxTransform.localPosition = new Vector3(playerHitBoxTransform.localPosition.x * (-1), playerHitBoxTransform.localPosition.y);

            prevhAxis = hAxis;
        }
    }

    void Jump()
    {
        animator.SetFloat("jumpSpeed", curSpeed);
        if (isJump)
        {
            if (HitboxController.hitboxYPosition > HitboxController.defalutHitboxYPosition)
            {
                HitboxController.hitboxYPosition += curSpeed;
                curSpeed -= Time.deltaTime * HALF;
            }
            else
            {
                HitboxController.hitboxYPosition = HitboxController.defalutHitboxYPosition;
                isJump = false;
            }
        }
        if (jumpDown)
        {
            if (!isJump)
            {
                curSpeed = jumpSpeed * 0.1f;
                HitboxController.hitboxYPosition += curSpeed;
                isJump = true;
                animator.SetTrigger("doJump");
            }
        }
    }

    void Attack()
    {
        attackDelay += Time.deltaTime * playerStat.attackSpeed;
        if (attackDown && isAttack && !nextCombo)
        {
            nextCombo = true;
            if (attackCount >= MAX_COMBO_COUNT)
                attackCount = 0;
        }
        if ((attackDown && !isAttack) || (attackDelay >= COMBO_INTERVAL && nextCombo))
        {
            StopAllAnimation();
            ComboAttack(attackCount++);
            attackDelay = 0;
            attackMoveSpeed = defaultAttackMoveSpeed;
        }
        else if (attackDelay >= COMBO_INTERVAL && !nextCombo)
        {
            isAttack = false;
            attackDelay = 0;
            attackCount = 0;
        }

    }

    void OnHitBox()
    {
        playerHitBox.SetActive(true);
    }

    void OffHitBox()
    {
        playerHitBox.SetActive(false);
    }

    void ComboAttack(int atkCnt)
    {
        animator.SetFloat("attackCount", atkCnt);
        animator.SetTrigger("doAttack");
        OnHitBox();
        Invoke("OffHitBox", 0.3f);
        nextCombo = false;
        isAttack = true;
    }

    void Hit(int damage)
    {
        playerStat.hp -= damage;
        UIManager.Instance.hpBar.fillAmount = (float)playerStat.hp / (float)playerStat.maxHp;
    }

    IEnumerator GainItem()
    {
        if (interactionDown)
        {
            DropItem[0].Gain();
            DropItem.RemoveAt(0);
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDropItem dropitem = other.GetComponent<IDropItem>();
        if (dropitem != null)
        {
            dropitem.Gain();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster"))
        {
            Hit(collision.gameObject.GetComponent<MonsterController>().stat.attack);
        }
    }
}
