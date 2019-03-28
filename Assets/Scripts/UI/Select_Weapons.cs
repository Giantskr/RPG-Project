using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Weapons : Select
{
    public GameObject UI_Selections;
    public GameObject UI_Weapons;

    void Start()
    {
        
    }

    void Update()
    {
        Selection();
        if (Input.GetButtonDown("Cancel"))
        {
            UI_Selections.SetActive(true);
            UI_Weapons.SetActive(false);
        }
		else if (Input.GetButtonDown("Submit"))
		{
			switch (states)
			{
				case 1: break;
			}
		}
	}
}
