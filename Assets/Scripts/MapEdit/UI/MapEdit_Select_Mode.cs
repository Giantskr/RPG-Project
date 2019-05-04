using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEdit_Select_Mode : Select
{
	public GameObject editMode;
	public GameObject tileWindow;
	public GameObject mapEditManager;

    void OnEnable()
    {
		states = 1;
    }

    void Update()
    {
		if (!GameManager.fading)
		{
			GetComponent<Image>().enabled = true;
			Selection();
			if (Input.GetButtonDown("Submit"))
			{
				switch (states)
				{
					case 1:
						tileWindow.SetActive(true);
						break;
					case 2:
						mapEditManager.GetComponent<MapEdit_Manager>().EnterPlayMode();
						break;
					case 3:
						mapEditManager.GetComponent<GameManager>().StartCoroutine("ChangeScene", "Start");
						break;
				}
				editMode.SetActive(false);
			}
			else if (Input.GetButtonDown("Cancel")) states = 3;
		}
		else GetComponent<Image>().enabled = false;
	}
}
