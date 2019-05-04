using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_Main : Select
{
    public GameObject Extra;
    public GameObject Options;
    public GameObject Menu;
    public GameManager gameManager;
	public AudioClip loadSound;

	void OnEnable()
	{
		states = 1;
	}
	void Start()
    {
		Player_Stats.lastScene = SceneManager.GetActiveScene().name;
    }
    void Update()
    {
        if (!GameManager.fading)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            Selection();
            if (Input.GetButtonDown("Submit"))
            {
                switch (states)
                {
                    case 1:
						GameManager.whichSound = 5;
						gameManager.GetComponent<AudioSource>().PlayOneShot(loadSound);
                        gameManager.StartCoroutine("ChangeScene", "OpenAni");
                        break;
                    case 2:
                        if (PlayerPrefs.GetString("PlayerInScene") != "")
						{
							GameManager.whichSound = 5;
							gameManager.GetComponent<AudioSource>().PlayOneShot(loadSound);
							Player_Stats.Load();
							gameManager.StartCoroutine("ChangeScene", PlayerPrefs.GetString("PlayerInScene"));
						}
						else GameManager.whichSound = 2;
						break;
					case 3:
						Extra.SetActive(true);
						Menu.SetActive(false);
						break;
					case 4:
                        Options.SetActive(true);
                        Menu.SetActive(false);
                        break;
                }
            }
        }
		else GetComponent<SpriteRenderer>().enabled = false;

	}
}
