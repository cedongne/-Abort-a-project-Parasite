using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public string objectName;
    public string type;
    public int grade;
    public int attack;
    public int hp;
    public float attackSpeed;
    public float moveSpeed;

    public Stat(string _objectName, string _type, int _grade, int _attack, int _hp, float _attackSpeed, float _moveSpeed) {
        objectName = _objectName;
        type = _type;
        grade = _grade;
        attack = _attack;
        hp = _hp;
        attackSpeed = _attackSpeed;
        moveSpeed = _moveSpeed;
    }


    public virtual void OnAttacked(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.attack);
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            OnDead(attacker);
        }
    }

    protected virtual void OnDead(Stat attacker)
    {
    }
}
