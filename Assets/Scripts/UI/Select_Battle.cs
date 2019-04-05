using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Battle : Select
{
	public GameObject BattleCommands;
	public GameObject BattleOrRun;

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
					
					break;
				case 2:
					
					break;
				case 3:
					
					break;
				case 4:
					
					break;
			}
		}
		else if (Input.GetButtonDown("Cancel"))
		{
			BattleOrRun.SetActive(true);
			BattleCommands.SetActive(false);
		}
	}
}
