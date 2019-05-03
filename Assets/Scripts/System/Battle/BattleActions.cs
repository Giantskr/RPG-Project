using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class BattleActions : MonoBehaviour
{
	public static GameObject player;
	public GameObject[] allmonsters;
	[Space]
	public Text messageLine1;
	public Text messageLine2;
	public Text HPText;
	public Text MPText;
	public static BattleActions battleAction;
	public GameObject Message;
	public GameObject BattleOrRun;

	int getExp = 0, getMoney = 0;

	public enum BattleState
	{
		battling,win,lose
	}
	public static BattleState battleState;

	public static List<GameObject> monsterInBattle;
	public static List<GameObject> turn;

	public static int turnNum = -1;
	public static bool endTurn = false;

	SkillData skills;

	private void Start()
	{
		player = GameObject.Find("PlayerInBattle");
		battleState = BattleState.battling;

		if (monsterInBattle == null) monsterInBattle = new List<GameObject>();
		if (turn == null) turn = new List<GameObject>();
		if (gameObject.name == "Battle")
		{
			battleAction = GetComponent<BattleActions>();
			Transform monsterParentObject = transform.GetChild(0);
			for (int i = 0; i < monsterParentObject.childCount; i++)
			{
				monsterInBattle.Add(monsterParentObject.GetChild(i).gameObject);
				turn.Add(monsterParentObject.GetChild(i).gameObject);
			}
			turn.Add(player);
			turn = turn.OrderByDescending(turn => turn.GetComponent<Monster>().info.AGI).ToList();
			battleAction.messageLine1.text = "";
			battleAction.messageLine2.text = "";
		}
		skills = LoadJson<SkillData>.LoadJsonFromFile("Skills");
	}


	void Update()
	{
		Player_Stats.HP = player.GetComponent<Monster>().info.HP;
		Player_Stats.MP = player.GetComponent<Monster>().info.MP;
		//battleAction.HPText.text = Player_Stats.HP + "/" + Player_Stats.maxHP;
		battleAction.MPText.text = Player_Stats.MP + "/" + Player_Stats.maxMP;
		if (gameObject.name == "Battle" && endTurn) NextTurn();
	}

	public void NextTurn()
	{
		endTurn = false;
		turnNum++;
		if (turnNum < turn.Count)
		{
			if (turn[turnNum] != null)
			{
				if (turn[turnNum] == player) PlayerUseSkill();
				else turn[turnNum].GetComponent<Monster>().MonsterUsingSkill();
			}
			else NextTurn();
		}
		else if (battleState == BattleState.battling) 
		{
			battleAction.BattleOrRun.SetActive(true);
			Message.SetActive(false);
			turn = turn.OrderByDescending(turn => turn.GetComponent<Monster>().info.AGI).ToList();
			turnNum = -1;
		}
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
				aPositive.GetComponent<Monster>().ifGuard = false;
				string aName;
				aName = aPositive.GetComponent<Monster>().info.monsterName;
				battleAction.messageLine1.text = aName + "使用了" + data.skillName + "！";
				battleAction.messageLine2.text = "";
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
					endTurn = true;
				}
				else
					switch (data.id)
					{
						case 1:
							bAction.StartCoroutine("TakeDamage", ReadFormula(data, aPositive, bNegative));
							break;
						case 2:
							StartCoroutine("DisplayMessage2", "本回合受到的伤害减少了！");
							aPositive.GetComponent<Monster>().ifGuard = true;
                            StartCoroutine("EndTurn");
							break;
					}
				break;
			}
	}
	
	IEnumerator DisplayMessage2(string message)
	{
		yield return new WaitForSeconds(0.75f);
		battleAction.messageLine2.text = message;
		battleAction.HPText.text = Player_Stats.HP.ToString() + "/" + Player_Stats.maxHP.ToString();
		yield return new WaitForSeconds(1f);
	}
	/// <summary>
	/// 受到伤害
	/// </summary>
	IEnumerator TakeDamage(int dmg)
	{
		if (GetComponent<Monster>().ifGuard) dmg /= 2;
		GetComponent<Monster>().info.HP -= dmg;
		string message;
		message = GetComponent<Monster>().info.monsterName;
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
				yield return new WaitForSeconds(0.5f);
				battleAction.messageLine1.text = "达拉崩吧倒下了！";
				battleAction.messageLine2.text = "";
				battleState = BattleState.lose;
			}
		}
		else
		{
			if (GetComponent<Monster>().info.HP <= 0) 
			{
				yield return new WaitForSeconds(0.5f);
				battleAction.messageLine1.text = GetComponent<Monster>().info.monsterName + "倒下了！";
				battleAction.messageLine2.text = "";
				getExp += gameObject.GetComponent<Monster>().info.getExp;
				getMoney += gameObject.GetComponent<Monster>().info.getMoney;
				monsterInBattle.Remove(gameObject);
				turn.Remove(gameObject);
				GetComponent<Animator>().enabled = true;
				if (monsterInBattle.Count == 0)
				{
					yield return new WaitForSeconds(1);
					battleAction.messageLine1.text =  "战斗胜利！";
					yield return new WaitForSeconds(0.75f);
					Player_Stats.EXP += getExp;
					Player_Stats.money += getMoney;
					battleAction.messageLine2.text = "获得了" + getExp.ToString() + "经验值与" + getMoney.ToString() + "金钱！";
					yield return new WaitForSeconds(1);
					battleState = BattleState.win;
				}
				yield return new WaitForSeconds(0.75f);
				endTurn = true;
                gameObject.SetActive(false);
			}
		}
		yield return new WaitForSeconds(1);
		endTurn = true;
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
					changedArray += aPositive.GetComponent<Monster>().info.ATK.ToString();
					break;
				case "a.def":
					changedArray += aPositive.GetComponent<Monster>().info.DEF.ToString();
					break;
				case "a.mat":
					changedArray += aPositive.GetComponent<Monster>().info.MAT.ToString();
					break;
				case "a.mdf":
					changedArray += aPositive.GetComponent<Monster>().info.MDF.ToString();
					break;
				case "a.agi":
					changedArray += aPositive.GetComponent<Monster>().info.AGI.ToString();
					break;
				case "a.mhp":
					changedArray += aPositive.GetComponent<Monster>().info.maxHP.ToString();
					break;
				case "a.mmp":
					changedArray += aPositive.GetComponent<Monster>().info.maxMP.ToString();
					break;
				case "a.hp":
					changedArray += aPositive.GetComponent<Monster>().info.HP.ToString();
					break;
				case "a.mp":
					changedArray += aPositive.GetComponent<Monster>().info.MP.ToString();
					break;
				case "b.atk":
					changedArray += bNegative.GetComponent<Monster>().info.ATK.ToString();
					break;
				case "b.def":
					changedArray += bNegative.GetComponent<Monster>().info.DEF.ToString();
					break;
				case "b.mat":
					changedArray += bNegative.GetComponent<Monster>().info.MAT.ToString();
					break;
				case "b.mdf":
					changedArray += bNegative.GetComponent<Monster>().info.MDF.ToString();
					break;
				case "b.agi":
					changedArray += bNegative.GetComponent<Monster>().info.AGI.ToString();
					break;
				case "b.mhp":
					changedArray += bNegative.GetComponent<Monster>().info.maxHP.ToString();
					break;
				case "b.mmp":
					changedArray += bNegative.GetComponent<Monster>().info.maxMP.ToString();
					break;
				case "b.hp":
					changedArray += bNegative.GetComponent<Monster>().info.HP.ToString();
					break;
				case "b.mp":
					changedArray += bNegative.GetComponent<Monster>().info.MP.ToString();
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
	public void PlayerUseSkill()
	{
		GetComponent<BattleActions>().UseSkill(Player_Stats.skillIdToUse, player, Player_Stats.target);
	}
    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(2f);
        endTurn = true;
    }
}
