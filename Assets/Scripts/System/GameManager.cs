using System.Collections;
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

	GameObject[] allMonsters;
	public static List<GameObject> monstersJoining;

    //public static Select_Esc instance = null;
    //public static Select_Esc GetInstance()
    //{
    //    return instance;
    //}

    //private void Awake()
    //{
    //    DontDestroyOnLoad(UI_Esc);
    //    if (instance == null)
    //        instance = UI_Esc.GetComponent<Select_Esc>();
    //    else if (instance != UI_Esc.GetComponent<Select_Esc>())
    //        Destroy(UI_Esc);
    //}

    void OnEnable()
    {
        if (cam.GetComponent<MapCamera>().sceneType == MapCamera.SceneType.GamePlay)
        {
            //allMonsters = UI_Battle.GetComponent<BattleActions>().allmonsters;
            //monstersJoining = new List<GameObject>();
            fading = true;
            inScene = true;
        }
        else
        {
            fading = false;
            inScene = false;
        }
        inBattle = false;
        SceneManager.sceneLoaded += OnSceneChange;
        au = GetComponent<AudioSource>();


        //	if (monstersJoining != null) monstersJoining.Clear();
        //	Debug.Log(allMonsters[0]);
        //	monstersJoining.Add(allMonsters[0]);
        //	StartBattle(monstersJoining);
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
		//else if (inBattle && !UI_Battle.activeInHierarchy) UI_Battle.SetActive(true);
    }
	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneChange;
	}
	public IEnumerator ChangeScene(string scene)
	{
		fading = true;
		fadingScreen.GetComponent<Animator>().Play("FadeToBlack");
		yield return new WaitForSeconds(0.8f);
		loading = SceneManager.LoadSceneAsync(scene);
	}
	void OnSceneChange(Scene next, LoadSceneMode mode)
	{
		if (Player_Stats.lastScene != null)
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
                        vector = Vector2.down; pos = new Vector2(-22f,25.5f);
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
                        vector = Vector2.left; pos = new Vector2(16f, -7f);
                    }
                    break;
                case "Cave":
                    if (Player_Stats.lastScene == "SnowMountain")
                        vector = Vector2.right; pos = new Vector2(-42f, 1f);
                    break;
            }
			SetPlayerOrientationAndPos(vector, pos);
			Resources.UnloadUnusedAssets();
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
		Vector2 startPos = Vector2.left * 100 * (monsters.Count - 1);
		foreach(GameObject monster in monsters)
		{
			Instantiate(monster, startPos, Quaternion.identity, UI_Battle.transform.GetChild(0));
			startPos += Vector2.right * 200;
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
