using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Options : Select 
{
    public GameObject Options;
    public GameObject Menu;

    void Start()
    {
        gameObject.transform.localPosition = new Vector3(0, -77.5f, 0);

    }

    void Update()
    {
        Selection();
		if (Input.GetButtonDown("Submit"))
		{
			switch (states)
			{
				case 1: break;
				case 2: break;
				case 3:
					Menu.SetActive(true);
					Options.SetActive(false);
					audioSource.Play();
					break;
			}
		}
    }
}
