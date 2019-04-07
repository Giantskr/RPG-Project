using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class BattleActions : MonoBehaviour
{
	public static GameObject player;
	public GameObject[] allmonsters;
	[Space]
	public Text text1;
	public Text text2;
	public static BattleActions battleAction;
	public GameObject Message;
	public GameObject BattleOrRun;

	public enum BattleState
	{
		battling,win,lose
	}
	public static BattleState battleState;

	public static List<GameObject> monsterInBattle;
	public static List<GameObject> Turn;

	int round = 0;
	bool ifGuard = false;

	SkillData skills;

	private void Start()
	{
		player = GameObject.Find("Player_Stats");
		battleState = BattleState.battling;

		if (monsterInBattle == null) monsterInBattle = new List<GameObject>();
		if (gameObject.name == "Battle")
		{
			if (battleAction == null) battleAction = GetComponent<BattleActions>();
			Transform monsterParentObject = transform.GetChild(0);
			for (int i = 0; i < monsterParentObject.childCount; i++)
				monsterInBattle.Add(monsterParentObject.GetChild(i).gameObject);
			battleAction.text1.text = "";
			battleAction.text2.text = "";
		}
		skills = LoadJson<SkillData>.LoadJsonFromFile("Skills");	
	}


	void Update()
	{
		
	}


	/// <summary>
	/// 使用技能
	/// </summary>
	/// <param name="id">技能编号</param>
	/// <param name="aPositive">使用者</param>
	/// <param name="bNegative">目标</param>
	public void UseSkill(byte id, GameObject aPositive, GameObject bNegative)
	{
		battleAction.Message.SetActive(true);
		BattleActions aAction = aPositive.GetComponent<BattleActions>();
		BattleActions bAction = bNegative.GetComponent<BattleActions>();
		bool miss = false;
		foreach (var data in skills.Skills)
			if (data.id == id)
			{
				string aName;
				if (aPositive == player) aName = "达拉崩吧";
				else aName = aPositive.GetComponent<Monster>().info.monsterName;
				battleAction.text1.text = aName + "使用了" + data.skillName + "！";
				battleAction.text2.text = null;
				float rand = Random.Range(0, 1);
				if (data.accuracy != 0)
				{
					float dodgeRate = 1;
					if (bNegative != player) dodgeRate = bNegative.GetComponent<Monster>().info.dodgeRate;
					if (rand > data.accuracy * dodgeRate) miss = true;
				}
				if (miss)
				{
					StartCoroutine("DisplayMessage2", "然而没有命中对方！");
				}
				else
					switch (data.id)
					{
						case 1:
							//bAction.TakeDamage(ReadFormula(data, aPositive, bNegative));
							bAction.StartCoroutine("TakeDamage", ReadFormula(data, aPositive, bNegative));
							break;
					}
				break;
			}
		//if (aPositive == player && bNegative != null) bNegative.GetComponent<BattleActions>().UseSkill(1, gameObject, player);
		if (aPositive == player && bNegative != null) bNegative.GetComponent<BattleActions>().Invoke("MonsterUsingSkill", 2f);
		//if (aPositive == player && bNegative != null) bNegative.GetComponent<BattleActions>().StartCoroutine("MonsterUsingSkill");
		else
		{
			battleAction.BattleOrRun.SetActive(true);
		}
	}
	
	IEnumerator DisplayMessage2(string message)
	{
		yield return new WaitForSeconds(0.75f);
		battleAction.text2.text = message;
		yield return new WaitForSeconds(1f);
	}
	/// <summary>
	/// 受到伤害
	/// </summary>
	IEnumerator TakeDamage(int dmg)
	{
		if (ifGuard) dmg /= 2;
		string message;
		if (gameObject == player)
		{
			Player_Stats.HP -= dmg;
			message = "达拉崩吧";
		}
		else
		{
			GetComponent<Monster>().HP -= dmg;
			message = GetComponent<Monster>().info.monsterName;
		}
		message += "受到了" + dmg + "点伤害！";
		StartCoroutine("DisplayMessage2", message);
		yield return new WaitForSeconds(1f);
		StartCoroutine("CheckHP");
	}
	IEnumerator CheckHP()
	{
		if (gameObject == player)
		{
			if (Player_Stats.HP <= 0)
			{
				battleAction.text1.text = "达拉崩吧倒下了！";
				battleAction.text2.text = "";
				battleState = BattleState.lose;
			}
		}
		else
		{
			if (GetComponent<Monster>().HP <= 0) 
			{
				battleAction.text1.text = GetComponent<Monster>().info.monsterName + "倒下了！";
				battleAction.text2.text = "";
				monsterInBattle.Remove(gameObject);
				GetComponent<Animator>().enabled = true;
				gameObject.transform.GetChild(0).gameObject.SetActive(true);
				gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Death");
				if (monsterInBattle.Count == 0)
				{
					yield return new WaitForSeconds(1);
					battleAction.text1.text =  "战斗胜利！";
					yield return new WaitForSeconds(2);
					battleState = BattleState.win;
				}
				yield return new WaitForSeconds(1);
				Destroy(gameObject);
			}
		}
		yield return null;
	}
	/// <summary>
	/// 读取技能伤害公式
	/// </summary>
	/// <param name="skill">技能</param>
	/// <param name="aPositive">使用者</param>
	/// <param name="bNegative">目标</param>
	/// <returns>伤害值</returns>
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
