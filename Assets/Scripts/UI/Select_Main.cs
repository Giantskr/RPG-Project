using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_Main : Select
{
    public GameObject Options;
    public GameObject Menu;

    void Start()
    {
        transform.localPosition = new Vector3(0, -77.5f, 0);
    }

    void Update()
    {
        selection();
		if (Input.GetButtonDown("Submit"))
		{
			switch (states)
			{
				case 1:
					SceneManager.LoadScene("Town");
					break;
				case 2: break;
				case 3:
					if (Input.GetButtonDown("Submit"))
					Options.SetActive(true);
					Menu.SetActive(false);
					audioSource.Play();
					break;
			}
		}
    }
}
