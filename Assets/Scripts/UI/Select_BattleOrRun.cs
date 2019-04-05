using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_BattleOrRun : Select
{
    public GameObject Battle;
    public GameObject BattleOrRun;
    public GameObject L;

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
					L.SetActive(true);
					BattleOrRun.SetActive(false);
					break;
				case 2:
					Battle.SetActive(false);
					GameManager.inBattle = false;
					GameManager.inScene = true;
					break;
			}
		}	
    }
}
