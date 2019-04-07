using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Battle : Select
{
	public GameObject BattleCommands;
	public GameObject BattleOrRun;
	public GameObject Characters;
	public GameObject monsterSelection;

	void Start()
    {
        
    }

    void Update()
    {
        Selection();
		if (Input.GetButtonDown("Submit"))
		{
			switch (states)
			{
				case 1:
					gameObject.SetActive(false);
					Characters.SetActive(false);
					monsterSelection.SetActive(true);
					Player_Stats.skillIdToUse = 1;
					break;
				case 2:
					
					break;
				case 3:
					BattleActions.player.GetComponent<BattleActions>().UseSkill(2, BattleActions.player, null);
					break;
				case 4:
					
					break;
			}
		}
		else if (Input.GetButtonDown("Cancel"))
		{
			BattleOrRun.SetActive(true);
			BattleCommands.SetActive(false);
			Characters.SetActive(false);
		}
	}
}
