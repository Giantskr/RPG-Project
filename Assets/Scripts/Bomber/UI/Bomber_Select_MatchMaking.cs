using UnityEngine;
using UnityEngine.UI;

public class Bomber_Select_MatchMaking : Select
{
	public GameManager gameManager;
	public GameObject socket;
	public Bomber_MatchManager matchManager;

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
					if (!Bomber_MatchManager.matching) matchManager.FindMatch();
					else GameManager.whichSound = 2;
					break;
				case 2:
					Bomber_MatchManager.matching = false;
					Destroy(socket);
					gameManager.StartCoroutine("ChangeScene", "Start");
					break;
				
			}
		}
		else if (Input.GetButtonDown("Cancel")) states = 2;
		if (GameManager.fading) GetComponent<Image>().enabled = false;
		else GetComponent<Image>().enabled = true;
	}
}
