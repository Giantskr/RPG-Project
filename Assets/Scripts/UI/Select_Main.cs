using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_Main : Select
{
    public GameObject Options;
    public GameObject Menu;
    public GameManager gameManager;
    void Start()
    {
		Player_Stats.lastScene = SceneManager.GetActiveScene().name;
        transform.localPosition = new Vector3(0, -77.5f, 0);
    }

    void Update()
    {
        if (!GameManager.fading)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Animator>().enabled = true;
            Selection();
            if (Input.GetButtonDown("Submit"))
            {
                switch (states)
                {
                    case 1:
                        gameManager.StartCoroutine("ChangeScene", "OpenAni");
                        break;
                    case 2:
                        if (PlayerPrefs.GetString("PlayerInScene") != "")
						{
							Player_Stats.Load();
							gameManager.StartCoroutine("ChangeScene", PlayerPrefs.GetString("PlayerInScene"));
						}
                        break;
                    case 3:
                        Options.SetActive(true);
                        Menu.SetActive(false);
                        break;
                }
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }
	
    }
}
