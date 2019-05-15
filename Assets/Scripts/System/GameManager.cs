using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public enum SceneType
	{
		GamePlay, Title, ExtraGame
	}
	public SceneType sceneTypeInInspector;
	public static SceneType sceneType;

	public GameObject UI_Esc;
	public GameObject UI_Battle;
	public GameObject UI_Store;
    public GameObject UI_Forge;
    public GameObject UI_Synthesis;
    public GameObject fadingScreen;
	public GameObject player;

    public static int whichSound=5;
    //protected AudioSource audioSource;
    public AudioClip[] UI_Sounds;

    public static bool inScene;
	public static bool inBattle;
	public static bool fading;
	AsyncOperation loading;

	protected AudioSource au;

	public static GameObject[] allMonsters;
	public static List<GameObject> monstersJoining;


    void OnEnable()
    {
		sceneType = sceneTypeInInspector;
        fading = true;
        if (sceneType == SceneType.GamePlay)
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
        SoundPlay();
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
				StartCoroutine("ChangeScene", "Start");
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
		if (Player_Stats.lastScene != null)
		{
			switch (next.name)
			{
				case "OpenAni":
					if (Player_Stats.lastScene == "Start")
					{
						Vector2 vector = new Vector2(PlayerPrefs.GetInt("PlayerOrientationX"), PlayerPrefs.GetInt("PlayerOrientationY"));
						Vector2 pos = new Vector2(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"));
						SetPlayerOrientationAndPos(vector, pos);
					}
					break;
				case "Store":
					if (Player_Stats.lastScene == "GrassLand")
						SetPlayerOrientationAndPos(Vector2.right, new Vector2(-10.5f, -1.5f));
                    else if (Player_Stats.lastScene == "SnowMountain")
						SetPlayerOrientationAndPos(Vector2.left, new Vector2(11.5f, -0.5f));
                    break;
				case "GrassLand":
					if (Player_Stats.lastScene == "Store")
						SetPlayerOrientationAndPos(Vector2.left, new Vector2(10.5f, 1.5f));
                    else if (Player_Stats.lastScene == "PalaceIn")
						SetPlayerOrientationAndPos(Vector2.down, new Vector2(-21.5f, 25.5f));
                    break;
                case "Town":
                    if (Player_Stats.lastScene == "PalaceOut")
						SetPlayerOrientationAndPos(Vector2.right, new Vector2(-6.5f, 1.5f));
                    break;
                case "PalaceOut":
                    if (Player_Stats.lastScene == "Town")
						SetPlayerOrientationAndPos(Vector2.up, new Vector2(0.5f, -7f));
                    break;
                case "PalaceIn":
                    if (Player_Stats.lastScene == "PalaceOut")
						SetPlayerOrientationAndPos(Vector2.up, new Vector2(0, -18.5f));
                    break;
                case "SnowMountain":
                    if (Player_Stats.lastScene == "Store")
						SetPlayerOrientationAndPos(Vector2.right, new Vector2(-16f, -2f));
                    else if (Player_Stats.lastScene == "Cave")
						SetPlayerOrientationAndPos(Vector2.left, new Vector2(15f, -7f));
                    break;
                case "Cave":
                    if (Player_Stats.lastScene == "SnowMountain")
						SetPlayerOrientationAndPos(Vector2.right, new Vector2(-42f, 1f));
                    break;
            }
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
    public void OpenForge()
    {
        inScene = false;
        UI_Forge.SetActive(true);
    }
    public void OpenSynthesis()
    {
        inScene = false;
        UI_Synthesis.SetActive(true);
    }
    public void SoundPlay()
    {
		if (whichSound != 5)
		{
			au.PlayOneShot(UI_Sounds[whichSound]);
			whichSound = 5;
		}
    }
}
