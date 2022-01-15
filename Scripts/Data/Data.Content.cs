using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	#region Stat
	[Serializable]
	public class Stat
	{
		public int maxHp;
		public int attack;
		public float attackSpeed;
		public float moveSpeed;
		public float criticalChance;
		public float coolDown;
	}

	[Serializable]
	public class StatData : ILoader<int, Stat>
	{
		public List<Stat> stats = new List<Stat>();

		public Dictionary<int, Stat> MakeDict()
		{
			Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
			foreach (Stat stat in stats)
				dict.Add(1, stat);
			return dict;
		}
	}
	#endregion
}