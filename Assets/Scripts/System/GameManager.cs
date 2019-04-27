﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject cam;
	public GameObject UI_Esc;
	public GameObject UI_Battle;
	public GameObject UI_Store;
	public GameObject fadingScreen;
	public GameObject player;

    public static int whichSound=5;
    //protected AudioSource audioSource;
    public AudioClip[] UI_Sounds;

    public static bool inScene;
	public static bool inBattle;
	public static bool fading;
	AsyncOperation loading;

	AudioSource au;

	public static GameObject[] allMonsters;
	public static List<GameObject> monstersJoining;


    void OnEnable()
    {
        fading = true;
        if (cam.GetComponent<MapCamera>().sceneType == MapCamera.SceneType.GamePlay)
        {
			if (allMonsters == null) allMonsters = UI_Battle.GetComponent<BattleActions>().allmonsters;
			if (monstersJoining == null) monstersJoining = new List<GameObject>();
			
            inScene = true;
        }
        else
        {
            inScene = false;
        }
        inBattle = false;
        SceneManager.sceneLoaded += OnSceneChange;
        au = GetComponent<AudioSource>();
	}

    void Update()
    {
        whichSound = SoundPlay(whichSound);
		if (Input.GetButtonDown("Cancel") && inScene && !fading)
		{
			inScene = false;
			UI_Esc.SetActive(true);
		}
		if (!inBattle && monstersJoining != null) monstersJoining = null;
		if (inBattle && BattleActions.battleState != BattleActions.BattleState.battling) 
		{
			if (BattleActions.battleState == BattleActions.BattleState.lose)
			{
				fadingScreen.GetComponent<Animator>().Play("FadeToBlack");
				ChangeScene("Start");
			}
			else if (BattleActions.battleState == BattleActions.BattleState.win)
			{
				inBattle = false;inScene = true;
				UI_Battle.SetActive(false);
			}
		}
    }
	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneChange;
	}
	public IEnumerator ChangeScene(string scene)
	{
		if (!fading)
		{
			fading = true;
			fadingScreen.GetComponent<Animator>().Play("FadeToBlack");
			yield return new WaitForSeconds(0.8f);
		}
		loading = SceneManager.LoadSceneAsync(scene);
	}
	void OnSceneChange(Scene next, LoadSceneMode mode)
	{
		if (Player_Stats.lastScene == "Start" && next.name != "OpenAni") 
		{
			Vector2 vector = new Vector2(PlayerPrefs.GetInt("PlayerOrientationX"), PlayerPrefs.GetInt("PlayerOrientationY"));
			Vector2 pos = new Vector2(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"));
			SetPlayerOrientationAndPos(vector, pos);
		}
		else if (Player_Stats.lastScene != null)
		{
			Vector2 vector = Vector2.up, pos = Vector2.zero;
			switch (next.name)
			{
				case "Store":
					if (Player_Stats.lastScene == "GrassLand")
                    {
                        vector = Vector2.right; pos = new Vector2(-10.5f, -1.5f);
                    }
                    if (Player_Stats.lastScene == "SnowMountain")
                    {
                        vector = Vector2.left; pos = new Vector2(11.5f, -0.5f);
                    }   
                    break;
				case "GrassLand":
					if (Player_Stats.lastScene == "Store")
                    {
                        vector = Vector2.left; pos = new Vector2(10.5f, 1.5f);
                    }
                    if (Player_Stats.lastScene == "PalaceIn")
                    {
                        vector = Vector2.down; pos = new Vector2(-21.5f,25.5f);
                    } 
                    break;
                case "Town":
                    if (Player_Stats.lastScene == "PalaceOut")
                    {
                        vector = Vector2.right; pos = new Vector2(-6.5f, 1.5f);
                    }
                    break;
                case "PalaceOut":
                    if (Player_Stats.lastScene == "Town")
                    {
                        vector = Vector2.up; pos = new Vector2(0.5f, -7f);
                    }
                    break;
                case "PalaceIn":
                    if (Player_Stats.lastScene == "PalaceOut")
                    {
                        vector = Vector2.up; pos = new Vector2(0, -18.5f);
                    }
                    break;
                case "SnowMountain":
                    if (Player_Stats.lastScene == "Store")
                    {
                        vector = Vector2.right; pos = new Vector2(-16f, -2f);
                    }
                    if (Player_Stats.lastScene == "Cave")
                    {
                        vector = Vector2.left; pos = new Vector2(15f, -7f);
                    }
                    break;
                case "Cave":
                    if (Player_Stats.lastScene == "SnowMountain")
                    {
                        vector = Vector2.right; pos = new Vector2(-42f, 1f);
                    }
                    break;
            }
			SetPlayerOrientationAndPos(vector, pos);
		}
		Player_Stats.lastScene = next.name;
	}
	/// <summary>
	/// 设定角色朝向与位置
	/// </summary>
	/// <param name="vector">角色朝向</param>
	/// <param name="pos">角色位置</param>
	public void SetPlayerOrientationAndPos(Vector2 vector, Vector2 pos)
	{
		Events playerEvent = player.GetComponent<Events>();
		player.transform.position = pos;
		playerEvent.faceOrientation = vector;
		playerEvent.SetSprite();
	}

	public void StartBattle(List<GameObject> monsters)
	{
		inScene = false;
		inBattle = true;
		Vector2 startPos = (Vector2)UI_Battle.transform.position + Vector2.left * 2 * (monsters.Count - 1);
		foreach(GameObject monster in monsters)
		{
			Instantiate(monster, startPos, Quaternion.identity, UI_Battle.transform.GetChild(0));
			startPos += Vector2.right * 4;
		}
		UI_Battle.SetActive(true);
	}
    public void OpenStore()
	{
		inScene = false;
		UI_Store.SetActive(true);
	}
    public int SoundPlay(int whichSound)
    {
        switch (whichSound)
        {
            case 5: break;
            case 0:
                au.PlayOneShot(UI_Sounds[0]);whichSound = 5;break;
            case 1:
                au.PlayOneShot(UI_Sounds[1]); whichSound = 5; break;
            case 2:
                au.PlayOneShot(UI_Sounds[2]); whichSound = 5; break;
            case 3:
                au.PlayOneShot(UI_Sounds[3]); whichSound = 5; break;
        }
        return whichSound;
    }
}
