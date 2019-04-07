using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BattleActions
{
	public MonsterInfo info;

	public int HP, MP;

	private void OnEnable()
	{
		HP = info.maxHP;
		MP = info.maxMP;
	}

	public void MonsterUsingSkill()
	{
		if (HP > 0) UseSkill(1, gameObject, player);
	} 
}
//[System.Serializable]
//public class MonsterData
//{
//	[System.Serializable]
//	public class MonsterInfo
//	{
//		public byte id;
//		public string monsterName;
//		//public string spritePath;
//		public int maxHP;
//		//public int HP;
//		public int maxMP;
//		//public int MP;
//		public int ATK;
//		public int DEF;
//		public int MAT;
//		public int MDF;
//		public int AGI;
//		public int getExp;
//		public int getMoney;
//	}
//	public MonsterInfo[] Monsters;
//}
[System.Serializable]
public class MonsterInfo
{
	public byte id;
	public string monsterName;
	public int maxHP;
	public int maxMP;
	public int ATK;
	public int DEF;
	public int MAT;
	public int MDF;
	public int AGI;
	public float dodgeRate;
	public int getExp;
	public int getMoney;
}