using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_BattleOrRun : Select
{
    public GameObject Battle;
    public GameObject BattleOrRun;
    public GameObject L;
    public GameObject Characters;
    public GameObject Message;
	public Text text1;
	public Text text2;

    void OnEnable()
    {
		states = 1;
    }

    void Update()
    {
        Selection();
		if (Input.GetButtonDown("Submit"))
		{
			switch (states)
			{
				case 1:
					Message.SetActive(false);
					L.SetActive(true);
					Characters.SetActive(true);
					BattleOrRun.SetActive(false);
					break;
				case 2:
					StartCoroutine("Run");
					break;
			}
		}	
    }
	IEnumerator Run()
	{
		Message.SetActive(true);
		text1.text = "成功撤退！";
		text2.text = "";
		yield return new WaitForSeconds(1);
		Battle.SetActive(false);
		GameManager.inBattle = false;
		GameManager.inScene = true;
	}
}
