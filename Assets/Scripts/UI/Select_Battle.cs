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
					Characters.SetActive(false);
					monsterSelection.SetActive(true);
					Player_Stats.skillIdToUse = 1;
                    gameObject.SetActive(false);
                    break;
				case 2:
					gameObject.SetActive(true);
					break;
				case 3:
                    Player_Stats.skillIdToUse = 2;
                    Player_Stats.target = BattleActions.player;
                    BattleCommands.SetActive(false);
                    BattleActions.endTurn = true;
                    break;
				case 4:
					gameObject.SetActive(true);
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
