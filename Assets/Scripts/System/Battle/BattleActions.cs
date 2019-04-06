using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using UnityEngine;

public class BattleActions : MonoBehaviour
{
	public static GameObject player;

	GameObject[] monsterInBattlle;

	//MonsterData monsters;
	SkillData skills;

	private void Awake()
	{
		player = GameObject.Find("Player_Stats");
		//monsters = LoadJson<MonsterData>.LoadJsonFromFile("Monsters");
		skills = LoadJson<SkillData>.LoadJsonFromFile("Skills");
	}
	/// <summary>
	/// 使用技能
	/// </summary>
	/// <param name="id">技能编号</param>
	/// <param name="aPositive">使用者</param>
	/// <param name="bNegative">目标</param>
	public void UseSkill(byte id, GameObject aPositive, GameObject bNegative)
	{
		BattleActions aAction = aPositive.GetComponent<BattleActions>();
		BattleActions bAction = bNegative.GetComponent<BattleActions>();
		foreach (var data in skills.Skills)
			if (data.id == id)
			{
				switch (data.id)
				{
					case 1:
						bAction.TakeDamage(ReadFormula(data, aPositive, bNegative));
						break;
				}
				break;
			}
	
		
	}
	/// <summary>
	/// 受到伤害
	/// </summary>
	void TakeDamage(int dmg)
	{
		if (gameObject == player) Player_Stats.HP -= dmg;
		else GetComponent<Monster>().HP -= dmg;
	}
	/// <summary>
	/// 读取技能伤害公式
	/// </summary>
	/// <param name="skill">技能</param>
	/// <param name="aPositive">使用者</param>
	/// <param name="bNegative">目标</param>
	/// <returns></returns>
	int ReadFormula(SkillData.SkillInfo skill, GameObject aPositive, GameObject bNegative)
	{
		string[] sArray = Regex.Split(skill.formula, " ", RegexOptions.IgnoreCase);
		string changedArray = null;
		foreach (string i in sArray)
		{
			if (i == ",") break;
			switch (i)
			{
				case "a.atk":
					if (aPositive == player) changedArray += Player_Stats.ATK.ToString();
					else changedArray += aPositive.GetComponent<Monster>().info.ATK.ToString();
					break;
				case "a.def":
					if (aPositive == player) changedArray += Player_Stats.DEF.ToString();
					else changedArray += aPositive.GetComponent<Monster>().info.DEF.ToString();
					break;
				case "a.mat":
					if (aPositive == player) changedArray += Player_Stats.MAT.ToString();
					else changedArray += aPositive.GetComponent<Monster>().info.MAT.ToString();
					break;
				case "a.mdf":
					if (aPositive == player) changedArray += Player_Stats.MDF.ToString();
					else changedArray += aPositive.GetComponent<Monster>().info.MDF.ToString();
					break;
				case "a.agi":
					if (aPositive == player) changedArray += Player_Stats.AGI.ToString();
					else changedArray += aPositive.GetComponent<Monster>().info.AGI.ToString();
					break;
				case "a.mhp":
					if (aPositive == player) changedArray += Player_Stats.maxHP.ToString();
					else changedArray += aPositive.GetComponent<Monster>().info.maxHP.ToString();
					break;
				case "a.mmp":
					if (aPositive == player) changedArray += Player_Stats.maxMP.ToString();
					else changedArray += aPositive.GetComponent<Monster>().info.maxMP.ToString();
					break;
				case "a.hp":
					if (aPositive == player) changedArray += Player_Stats.HP.ToString();
					else changedArray += aPositive.GetComponent<Monster>().HP.ToString();
					break;
				case "a.mp":
					if (aPositive == player) changedArray += Player_Stats.MP.ToString();
					else changedArray += aPositive.GetComponent<Monster>().MP.ToString();
					break;
				case "b.atk":
					if (bNegative == player) changedArray += Player_Stats.ATK.ToString();
					else changedArray += bNegative.GetComponent<Monster>().info.ATK.ToString();
					break;
				case "b.def":
					if (bNegative == player) changedArray += Player_Stats.DEF.ToString();
					else changedArray += bNegative.GetComponent<Monster>().info.DEF.ToString();
					break;
				case "b.mat":
					if (bNegative == player) changedArray += Player_Stats.MAT.ToString();
					else changedArray += bNegative.GetComponent<Monster>().info.MAT.ToString();
					break;
				case "b.mdf":
					if (bNegative == player) changedArray += Player_Stats.MDF.ToString();
					else changedArray += bNegative.GetComponent<Monster>().info.MDF.ToString();
					break;
				case "b.agi":
					if (bNegative == player) changedArray += Player_Stats.AGI.ToString();
					else changedArray += bNegative.GetComponent<Monster>().info.AGI.ToString();
					break;
				case "b.mhp":
					if (bNegative == player) changedArray += Player_Stats.maxHP.ToString();
					else changedArray += bNegative.GetComponent<Monster>().info.maxHP.ToString();
					break;
				case "b.mmp":
					if (bNegative == player) changedArray += Player_Stats.maxMP.ToString();
					else changedArray += bNegative.GetComponent<Monster>().info.maxMP.ToString();
					break;
				case "b.hp":
					if (bNegative == player) changedArray += Player_Stats.HP.ToString();
					else changedArray += bNegative.GetComponent<Monster>().HP.ToString();
					break;
				case "b.mp":
					if (bNegative == player) changedArray += Player_Stats.MP.ToString();
					else changedArray += bNegative.GetComponent<Monster>().MP.ToString();
					break;
				default:
					changedArray += i;
					break;
			}
		}
		DataTable eval = new DataTable();
		int result = (int)eval.Compute(changedArray, "");
		float dispersion = float.Parse(sArray[sArray.Length - 1]) / 100;
		float rate = 1 + Random.Range(-dispersion, dispersion);
		return System.Math.Max(0, (int)(result * rate));
	}	
}
