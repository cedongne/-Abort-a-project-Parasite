using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
	public int criticalChance;
	public int coolDown;
	public int gold;
	public PlayerStat(string _objectName, string _type, int _grade, int _attack, int _hp, float _attackSpeed, float _moveSpeed, int _criticalChance, int _coolDown) :
		base(_objectName, _type, _grade, _attack, _hp, _attackSpeed, _moveSpeed)
	{ }
	public PlayerStat(string _objectName, string _type, int _grade, int _attack, int _hp, float _attackSpeed, float _moveSpeed, int _criticalChance, int _coolDown, int _gold) : 
		base(_objectName, _type, _grade, _attack, _hp, _attackSpeed, _moveSpeed)
	{
		criticalChance = _criticalChance;
		coolDown = _coolDown;
		gold = _gold;
	}

	/*
	public void SetStat(int level)
	{
		Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
		Data.Stat stat = dict[level];
		Hp = stat.maxHp;
		MaxHp = stat.maxHp;
		Attack = stat.attack;
	}
	*/
	protected override void OnDead(Stat attacker)
	{
		Debug.Log("Player Dead");
	}
}
