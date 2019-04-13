using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Stats : BattleActions
{
	public static int maxHP = 450;
	public static int maxMP = 90;
	public static int HP = maxHP;
	public static int MP = maxMP;
	public static int ATK = 16;
	public static int DEF = 18;
	public static int MAT = 16;
	public static int MDF = 14;
	public static int AGI = 32;
	public static int LUC = 5;
	public static int level = 1;
	public static int EXP = 0;
	public static int EXPToNextLevel = 50;
	public static int money = 1000;
    public static float volume=0.5f;

	public static string lastScene;

	public static int[] switchListInt;
	public static Player_Stats instance = null;

	SkillData Skill;
	public static byte skillIdToUse = 0;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

        AudioListener.volume = volume;

        if (switchListInt == null) switchListInt = new int[50];

		if (Skill == null) Skill = LoadJson<SkillData>.LoadJsonFromFile("Skills");
	}
    public static void Save()
    {
		Transform playerPos = FindObjectOfType<Player_Control>().transform;
		Events playerEvent = FindObjectOfType<Player_Control>().GetComponent<Events>();

        PlayerPrefs.SetInt("maxHP", maxHP);
        PlayerPrefs.SetInt("maxMP", maxMP);
        PlayerPrefs.SetInt("HP", HP);
        PlayerPrefs.SetInt("MP", MP);
        PlayerPrefs.SetInt("ATK", ATK);
        PlayerPrefs.SetInt("DEF", DEF);
        PlayerPrefs.SetInt("MAT", MAT);
        PlayerPrefs.SetInt("MDF", MDF);
        PlayerPrefs.SetInt("AGI", AGI);
        PlayerPrefs.SetInt("LUC", LUC);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("EXP", EXP);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetString("PlayerInScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("PlayerPosX", playerPos.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerPos.position.y);
        PlayerPrefs.SetInt("PlayerOrientationX", (int)playerEvent.faceOrientation.x);
        PlayerPrefs.SetInt("PlayerOrientationY", (int)playerEvent.faceOrientation.y);
		PlayerPrefs.SetFloat("volume", volume);
		for (int i = 0; i < switchListInt.Length; i++)
			PlayerPrefs.SetInt("switchListInt" + i.ToString(), switchListInt[i]);

		PlayerPrefs.Save();
	}
    public static void Load()
    {
        maxHP = PlayerPrefs.GetInt("maxHP");
        maxMP= PlayerPrefs.GetInt("maxMP");
        HP= PlayerPrefs.GetInt("HP");
        MP =PlayerPrefs.GetInt("MP");
        ATK =PlayerPrefs.GetInt("ATK");
        DEF= PlayerPrefs.GetInt("DEF");
        MAT= PlayerPrefs.GetInt("MAT");
        MDF =PlayerPrefs.GetInt("MDF");
        AGI= PlayerPrefs.GetInt("AGI");
        LUC= PlayerPrefs.GetInt("LUC" );
        level= PlayerPrefs.GetInt("level");
        EXP= PlayerPrefs.GetInt("EXP" );
        money= PlayerPrefs.GetInt("money");
		volume = PlayerPrefs.GetFloat("Volume");
		for (int i = 0; i < switchListInt.Length; i++)
			switchListInt[i] = PlayerPrefs.GetInt("switchListInt" + i.ToString());
    }

    public static Player_Stats GetInstance()
	{
		return instance;
	}

	void OnEnable()
    {
		
	}

    void Update()
    {
        AudioListener.volume = volume;
    }
	public void PlayerUseSkill(GameObject monster)
	{
		GetComponent<BattleActions>().UseSkill(skillIdToUse, player, monster);
	}
    
}
