using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BattleActions
{
	public MonsterInfo info;

	public bool ifGuard = false;

	private void OnEnable()
	{
		if (gameObject.name == "PlayerInBattle")
		{
			info.maxHP = Player_Stats.maxHP;
			info.maxMP = Player_Stats.maxMP;
			info.HP = Player_Stats.HP;
			info.MP = Player_Stats.MP;
			info.ATK = Player_Stats.ATK;
			info.DEF = Player_Stats.DEF;
			info.MAT = Player_Stats.MAT;
			info.MDF = Player_Stats.MDF;
			info.AGI = Player_Stats.AGI;
		}
		else
		{
			info.HP = info.maxHP;
			info.MP = info.maxMP;
		}
	}

	public void MonsterUsingSkill()
	{
		if (info.HP > 0)
		{
			UseSkill(1, gameObject, player);
			gameObject.transform.GetChild(0).gameObject.SetActive(true);
			gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Attack");
			//Invoke("DisableFlash", 0.35f);
		}
	}
	void DisableFlash()
	{
		gameObject.transform.GetChild(0).gameObject.SetActive(false);
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
	public int HP;
	public int MP;
	public int ATK;
	public int DEF;
	public int MAT;
	public int MDF;
	public int AGI;
	public float dodgeRate;
	public int getExp;
	public int getMoney;
}