using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEdit_Select_Mode : Select
{
	public GameObject editMode;
	public GameObject tileWindow;
	public GameObject mapEditManager;
    void Start()
    {
        
    }

    void Update()
    {
		if (!GameManager.fading)
		{
			GetComponent<Image>().enabled = true;
			GetComponent<Animator>().enabled = true;
			Selection();
			if (Input.GetButtonDown("Submit"))
			{
				switch (states)
				{
					case 1:
						tileWindow.SetActive(true);
						editMode.SetActive(false);
						break;
					case 2:
						
						break;
					case 3:
						mapEditManager.GetComponent<GameManager>().StartCoroutine("ChangeScene", "Start");
						break;
				}
			}
		}
		else
		{
			GetComponent<Image>().enabled = false;
			GetComponent<Animator>().enabled = false;
		}
	}
}
