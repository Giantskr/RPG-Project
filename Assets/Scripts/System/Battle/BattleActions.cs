using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class BattleActions : MonoBehaviour
{
	GameObject[] monsterInBattlle;

	//MonsterData monsters;
	SkillData skills;

	private void Awake()
	{
		//monsters = LoadJson<MonsterData>.LoadJsonFromFile("Monsters");
		skills = LoadJson<SkillData>.LoadJsonFromFile("Skills");
	}

	public void UseSkill(byte id)
	{
		foreach (var data in skills.Skills)
			if (data.id == id)
			{
				ReadFormula(data);
				switch (data.id)
				{
					case 1:
						DealDamage(1);
						break;
				}
				break;
			}
	
		
	}

	void DealDamage(int dmg)
	{

	}

	int ReadFormula(SkillData.SkillInfo skill)
	{
		string[] sArray = Regex.Split(skill.formula, " ", RegexOptions.IgnoreCase);
		string changedArray = null;
		foreach (string i in sArray)
		{
			if (i == ",") break;
			switch (i)
			{
				case "a.atk":break;
			}
			changedArray += i;
		}
		Debug.Log(changedArray);
		return 233;
	}

	void TakeDamage(int dmg)
	{

	}
}
