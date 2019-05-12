using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomber_Manager : MonoBehaviour
{
	public enum BomberState
	{
		Start,InGame,End
	}
	public static BomberState bomberState;
	public GameObject bombPrefab;
	public GameObject player;
	public GameObject otherPlayer;
	public Transform playerBombParent;
	public Transform otherBombParent;
	public Text timer;
	public Text GameTipText;

	float remainingTime = 180;
	public static bool playerAlive=true;
	public static bool enemyAlive=true;
	

    void OnEnable()
    {
		bomberState = BomberState.Start;
		playerAlive = true; enemyAlive = true;
}

    void Update()
    {
		CheckGameState();
    }

	public void PlaceBomb(Vector2 pos, int range, bool self)
	{
		Transform bombParent;
		if (self) bombParent = playerBombParent;
		else bombParent = otherBombParent;
		GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity, bombParent);
		bomb.GetComponent<Bomber_Bomb>().range = range;
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
				if (remainingTime <= 0 || !playerAlive || !enemyAlive) bomberState = BomberState.End;
				break;
			case BomberState.End:

				break;
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
