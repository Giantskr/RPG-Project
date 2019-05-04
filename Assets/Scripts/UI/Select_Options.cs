using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Options : Select 
{
    public GameObject Options;
    public GameObject Menu;
    int vol;
    void OnEnable()
    {
		states = 1;
    }

	void Update()
	{
		Selection();
		vol = (int)(Player_Stats.volume * 100 + 0.1);
		Selections[0].GetComponent<Text>().text = "< " + vol + " >";
		if (Input.GetButtonDown("Submit"))
		{
			switch (states)
			{
				case 2:
					Menu.SetActive(true);
					Options.SetActive(false);
					break;
			}
		}
		else if (Input.GetButtonDown("Cancel"))
		{
			Menu.SetActive(true);
			Options.SetActive(false);
		}
	}
	protected override void Selection()	//为实现左右调节音量而重写方法
	{
		if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
		{
			if (states != 1)
			{
				if ((Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Horizontal") > 0) && states < OptionNum) states++;
				else if ((Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Horizontal") < 0) && states > 1) states--;
			}
			else
			{
				if (Input.GetAxisRaw("Vertical") < 0) states++;
				else if (Input.GetAxisRaw("Horizontal") > 0)
				{
					if (Player_Stats.volume < 1) Player_Stats.volume += 0.1f;
					else Player_Stats.volume = 0;
				}
				else if(Input.GetAxisRaw("Horizontal") < 0)
				{
					if (Player_Stats.volume > 0) Player_Stats.volume -= 0.1f;
					else Player_Stats.volume = 1;
				}
			}
			GameManager.whichSound = 0;
		}
		else if (Input.GetButtonDown("Cancel")) GameManager.whichSound = 3;
		else if (Input.GetButtonDown("Submit")) GameManager.whichSound = 1;
		transform.localPosition = Selections[states - 1].transform.localPosition;
	}
}
