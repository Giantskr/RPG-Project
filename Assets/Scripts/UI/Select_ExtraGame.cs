using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_ExtraGame : Select
{
	public GameObject Menu;
	public GameObject Extra;
	public GameManager gameManager;

    void OnEnable()
    {
		GetComponent<SpriteRenderer>().enabled = true;
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
					gameManager.StartCoroutine("ChangeScene", "MapEdit");
					break;
				case 2:
					//gameManager.StartCoroutine("ChangeScene", "MapEdit");
					break;
				case 3:
					Menu.SetActive(true);
					Extra.SetActive(false);
					break;
			}
		}
		else if (Input.GetButtonDown("Cancel"))
		{
			Menu.SetActive(true);
			Extra.SetActive(false);
		}
		if (GameManager.fading) GetComponent<SpriteRenderer>().enabled = false;
	}
}
