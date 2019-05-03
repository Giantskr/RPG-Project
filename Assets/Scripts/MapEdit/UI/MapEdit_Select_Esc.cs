using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdit_Select_Esc : Select
{
	public GameObject escWindow;
	public GameObject editMode;
	public GameObject tileWindow;
	public GameObject mapEditManager;

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
					tileWindow.SetActive(true);
					break;
				case 2:
					editMode.SetActive(true);
					break;
				case 3:
					mapEditManager.GetComponent<GameManager>().StartCoroutine("ChangeScene", "Start");
					break;
			}
			mapEditManager.GetComponent<MapEdit_Manager>().ExitPlayMode();
			escWindow.SetActive(false);
		}
		else if (Input.GetButtonDown("Cancel"))
		{
			GameManager.inScene = true;
			escWindow.SetActive(false);
		}
	}
}
