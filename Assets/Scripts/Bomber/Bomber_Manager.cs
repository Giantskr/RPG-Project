using System.Text.RegularExpressions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bomber_Manager : MonoBehaviour
{
	public enum BomberState
	{
		Start,InGame,End
	}
	public static BomberState bomberState;
	Socket_Client socket;
	public GameManager gameManager;

	public GameObject bombPrefab;
	public GameObject itemPrefab;
	public GameObject player;
	public GameObject otherPlayer;
	public Transform playerBombParent;
	public Transform otherBombParent;
	public Text timer;
	public Text gameTipText;
	public GameObject chatUI;
	public Text[] chatText;
	public InputField messageSendingField;

	public static Vector2 playerPos = new Vector2(-7.5f, 4.5f);
	public static Vector2 enemyPos = new Vector2(8.5f, -5.5f);
	float remainingTime = 180;
	float timeSinceLastChat = 10;
	public static bool playerAlive = true;
	public static bool enemyAlive = true;
	public static bool chatting = false;

	void OnEnable()
    {
		socket = FindObjectOfType<Socket_Client>();
		player.transform.position = playerPos;
		otherPlayer.transform.position = enemyPos;
		playerAlive = true; enemyAlive = true; chatting = false;
		chatText[0].text = ""; chatText[1].text = "";
		bomberState = BomberState.Start;
	}

    void Update()
    {
		CheckGameState();
		ChatSystem();
    }

	public void PlaceBomb(Vector2 pos, int range, bool self)
	{
		LayerMask mask = LayerMask.GetMask("Obstacle");
		Collider2D hit = Physics2D.OverlapPoint(pos, mask);
		if (!hit || hit.tag != "Bomb")
		{
			Transform bombParent;
			if (self) bombParent = playerBombParent;
			else bombParent = otherBombParent;
			GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity, bombParent);
			bomb.GetComponent<Bomber_Bomb>().range = range;
		}
	}
	void CheckGameState()
	{
		switch (bomberState)
		{
			case BomberState.Start:
				if (!GameManager.fading)
				{
					GameManager.inScene = true;
					bomberState = BomberState.InGame;
				}
				break;
			case BomberState.InGame:
				remainingTime -= Time.deltaTime;
				int min = (int)(remainingTime / 60);
				int second = (int)(remainingTime - min * 60);
				timer.text = string.Format("{0}:{1:00}", min, second);
				if (remainingTime <= 0 || !playerAlive || !enemyAlive)
				{
					if (remainingTime <= 0) gameTipText.text = "时间到！";
					else
					{
						if (!playerAlive && !enemyAlive) gameTipText.text = "同归于尽！";
						else if (!playerAlive && enemyAlive) gameTipText.text = "失败！";
						else gameTipText.text = "胜利！";
					}
					bomberState = BomberState.End;
				} 
				break;
			case BomberState.End:
				StartCoroutine("ExitMatch");
				break;
		}
	}

	void ChatSystem()
	{
		if (timeSinceLastChat < 10.05f) timeSinceLastChat += Time.deltaTime;
		if (timeSinceLastChat < 10) chatUI.SetActive(true);
		else if (!chatting) chatUI.SetActive(false);
		if ((Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.Return)) && !chatting) 
		{
			Input.ResetInputAxes();
			chatUI.SetActive(true);
			messageSendingField.gameObject.SetActive(true);
			messageSendingField.ActivateInputField();
			chatting = true;
		}
		else if (Input.GetKeyDown(KeyCode.Return) && chatting)
		{
			Input.ResetInputAxes();
			if (messageSendingField.text != "")
			{
				DisplayChat(true, messageSendingField.text);
				timeSinceLastChat = 0;
				socket.SendData("Chat," + messageSendingField.text + ",");
			}
			messageSendingField.DeactivateInputField();
			messageSendingField.text = "";
			messageSendingField.gameObject.SetActive(false);
			chatting = false;
		}
	}
	public void DisplayChat(bool self,string ms)
	{
		string name;
		if (self) name = Socket_Client.userName;
		else
		{
			string[] sArray = Regex.Split(ms, ",", RegexOptions.IgnoreCase);
			ms = sArray[1];
			name = Socket_Client.enemyName;
		}
		if (chatText[0].text == "") chatText[0].text = name + ": " + ms;
		else
		{
			chatText[1].text = chatText[0].text;
			chatText[0].text = name + ": " + ms;
		}
	}

	IEnumerator ExitMatch()
	{
		yield return new WaitForSeconds(5);
		socket.SendData("Match,Cancel,");
		gameManager.StartCoroutine("ChangeScene", "Bomber_MatchMaking");
	}

	public void EnemyAction(string ms)
	{
		Debug.Log("EnemyAction: " + ms);
		otherPlayer.GetComponent<Bomber_Player>().EnemyControl(ms);
	}

	public void ChangeItemState(bool spawn, string ms)
	{
		string[] sArray = Regex.Split(ms, ",", RegexOptions.IgnoreCase);
		Vector2 pos = new Vector2(float.Parse(sArray[1]), float.Parse(sArray[2]));
		if (spawn)
		{
			int num = int.Parse(sArray[3]);
			GameObject item = Instantiate(itemPrefab, pos, Quaternion.identity);
			item.GetComponent<Bomber_Item>().spriteNum = num;
		}
		else
		{
			Collider2D[] hit = Physics2D.OverlapPointAll(pos);
			if (hit != null)
				foreach (Collider2D i in hit)
					if (i.tag == "Item") Destroy(i);
		}
	}
	public void DestroyBrick(bool self, string ms)
	{
		string[] sArray = Regex.Split(ms, ",", RegexOptions.IgnoreCase);
		Vector2 pos = new Vector2(float.Parse(sArray[1]), float.Parse(sArray[2]));
		if (FindObjectOfType<Bomber_Brick>() != null)
			FindObjectOfType<Bomber_Brick>().DestroyBrick(self, pos);
	}
}
