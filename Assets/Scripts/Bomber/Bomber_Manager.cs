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

	public GameObject bombPrefab;
	public GameObject player;
	public GameObject otherPlayer;
	public Transform playerBombParent;
	public Transform otherBombParent;
	public Text timer;
	public Text gameTipText;
	public GameObject chatUI;
	public InputField messageSendingField;

	float remainingTime = 180;
	float timeSinceLastChat = 10;
	public static bool playerAlive=true;
	public static bool enemyAlive=true;
	public static bool chatting = false;

	void OnEnable()
    {
		socket = FindObjectOfType<Socket_Client>();
		bomberState = BomberState.Start;
		playerAlive = true; enemyAlive = true; chatting = false;
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
			messageSendingField.DeactivateInputField();
			messageSendingField.text = "";
			messageSendingField.gameObject.SetActive(false);
			chatting = false;
		}
	}

	void ExitMatch()
	{

	}
	void SetPlayerPos()
	{

	}
	void GetPlayerPos()
	{

	}
}
