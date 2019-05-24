using UnityEngine;
using UnityEngine.UI;

public class Bomber_Select_MatchMaking : Select
{
	public GameManager gameManager;
	public Bomber_MatchManager matchManager;
	public Text findMatchText;

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
					if (!Bomber_MatchManager.matching)
					{
						findMatchText.text = "取消匹配";
						matchManager.FindMatch();
					}
					else
					{
						findMatchText.text = "开始匹配";
						matchManager.CancelMatch();
					}
					break;
				case 2:
					Bomber_MatchManager.matching = false;
					gameManager.StartCoroutine("ChangeScene", "Start");
					break;
				
			}
		}
		else if (Input.GetButtonDown("Cancel")) states = 2;
		if (GameManager.fading) GetComponent<Image>().enabled = false;
		else GetComponent<Image>().enabled = true;
	}
}
