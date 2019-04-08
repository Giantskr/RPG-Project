using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Monster : Select
{
	public GameObject monsterSelection;
	public GameObject Characters;
	public GameObject ActionSelection;
	public GameObject BattleCommands;

	int[] indexes;

	void Start()
    {
		if (indexes == null) indexes = new int[8];
		DisplayMonsterList();
		states = 1;
	}

    void Update()
    {
		if (BattleActions.monsterInBattle.Count != OptionNum)
			DisplayMonsterList();
		Selection();
		foreach (GameObject monster in BattleActions.monsterInBattle)
		{
			if (BattleActions.monsterInBattle.IndexOf(monster) == indexes[states-1])
				monster.transform.GetChild(0).gameObject.SetActive(true);
			else monster.transform.GetChild(0).gameObject.SetActive(false);
		}
		
		if (Input.GetButtonDown("Submit"))
		{
			foreach (GameObject monster in BattleActions.monsterInBattle)
			{
				BattleCommands.SetActive(false);
				if (BattleActions.monsterInBattle.IndexOf(monster) == indexes[states-1])
				{
					monster.transform.GetChild(0).gameObject.SetActive(false);
					ActionSelection.SetActive(true);
					BattleActions.player.GetComponent<BattleActions>().UseSkill(Player_Stats.skillIdToUse, BattleActions.player, monster);
					Characters.SetActive(true);
					monsterSelection.SetActive(false);
				}
			}
		}
		else if (Input.GetButtonDown("Cancel"))
		{
            foreach (GameObject monster in BattleActions.monsterInBattle)
                monster.transform.GetChild(0).gameObject.SetActive(false);               
            monsterSelection.SetActive(false);
			ActionSelection.SetActive(true);
			Characters.SetActive(true);
			Player_Stats.skillIdToUse = 1;
		}
	}
	void DisplayMonsterList()
	{
		OptionNum = BattleActions.monsterInBattle.Count;
		if (states < OptionNum) states = 1;
		int i = 0;
		for (i = 8; i > 0; i--) indexes[i - 1] = -1;
		foreach (GameObject monster in BattleActions.monsterInBattle)
		{
			Selections[i].GetComponent<Text>().text = monster.GetComponent<Monster>().info.monsterName;
			indexes[i] = BattleActions.monsterInBattle.IndexOf(monster);
			i++;
		}
		for (; i < 8; i++) Selections[i].GetComponent<Text>().text = "";
	}
}
