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

	public static bool inScene;
	public static bool inBattle;	
	public static bool fading;
	AsyncOperation loading;

	AudioSource au;

	GameObject[] allMonsters;
	public static List<GameObject> monstersJoining;
	//public static GameManager instance = null;
	//void Awake()
	//{
	//	DontDestroyOnLoad(gameObject);
	//	if (instance == null)
	//		instance = this;
	//	else if (instance != this)
	//		Destroy(gameObject);
	//}
	//public static GameManager GetInstance()
	//{
	//	return instance;
	//}

	void OnEnable()
    {
		if (cam.GetComponent<MapCamera>().sceneType == MapCamera.SceneType.GamePlay)
		{
			allMonsters = UI_Battle.GetComponent<BattleActions>().allmonsters;
			monstersJoining = new List<GameObject>();
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


		if (monstersJoining != null) monstersJoining.Clear();
		Debug.Log(allMonsters[0]);
		monstersJoining.Add(allMonsters[0]);
		StartBattle(monstersJoining);
	}

    void Update()
    {
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
						vector = Vector2.right; pos = new Vector2(-11.5f, -1.5f);
					break;
				case "GrassLand":
					if (Player_Stats.lastScene == "Store")
						vector = Vector2.left; pos = new Vector2(10.5f, 1.5f);
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
}
