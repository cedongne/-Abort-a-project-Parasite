using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterStat : Stat
{
	public MonsterStat(string _objectName, string _type, int _grade, int _attack, int _hp, float _attackSpeed, float _moveSpeed) :
		base(_objectName, _type, _grade, _attack, _hp, _attackSpeed, _moveSpeed)
	{ }

	protected override void OnDead(Stat attacker)
	{
		Debug.Log("Player Dead");
	}
}
