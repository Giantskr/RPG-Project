using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Select_Esc :Select
{
	public GameObject UI_Esc;

    public GameObject UI_Objects;
    public GameObject UI_Skills;
    public GameObject UI_Weapons;
    public GameObject UI_States;
    public GameObject UI_Settings;
    public GameObject UI_Save;

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
					UI_Objects.SetActive(true);
					gameObject.SetActive(false);
					break;
				case 2:
					UI_Skills.SetActive(true);
					gameObject.SetActive(false);
					break;
				case 3:
					UI_Weapons.SetActive(true);
					gameObject.SetActive(false);
					break;
				case 4:
					UI_States.SetActive(true);
					gameObject.SetActive(false);
					break;
				case 5:
					UI_Settings.SetActive(true);
					gameObject.SetActive(false);
					break;
				case 6: break;
				case 7:
					SceneManager.LoadScene("Start");
					break;
			}
		}
		else if (Input.GetButtonDown("Cancel"))
		{
			GameManager.inScene = true;
			UI_Esc.SetActive(false);
		}
    }
}
