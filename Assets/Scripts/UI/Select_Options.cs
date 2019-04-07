using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Options : Select 
{
    public GameObject Options;
    public GameObject Menu;
    int vol;
    void Start()
    {
        gameObject.transform.localPosition = new Vector3(0, -77.5f, 0);

    }

    void Update()
    {
        Selection();
        vol = (int)(Player_Stats.volumn * 100f);
        Selections[0].transform.GetChild(0).GetComponent<Text>().text = "音量  < " +vol + " > ";

        switch (states)
        {
            case 1:
                //if (Input.GetButtonDown("Return") && Player_Stats.volumn >= 0.1f)
                //{
                //    Player_Stats.volumn = 0f;
                //}
                if (Input.GetButtonDown("Submit") && Player_Stats.volumn <= 1.01f)
                {
                    Player_Stats.volumn += 0.1f;
                    if (Player_Stats.volumn > 1.01f)
                    {
                        Player_Stats.volumn = 0f;
                    }
                }
               
                    break;
            case 2: break;
            case 3:
                if (Input.GetButtonDown("Submit"))
                {
                    Menu.SetActive(true);
                    Options.SetActive(false);
                    //audioSource.Play();
                }
                break;
        }
			
		
		 if (Input.GetButtonDown("Cancel"))
		{
			Menu.SetActive(true);
			Options.SetActive(false);
		}

	}
}
