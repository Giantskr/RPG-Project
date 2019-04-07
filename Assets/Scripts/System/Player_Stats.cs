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
	public static int Money = 1000;
    public static float volumn=0.5f;

	public static string lastScene;

	public static int[] switchListInt;
	public static Player_Stats instance = null;

	SkillData Skill;
	public static byte skillIdToUse = 0;

	void Awake()
	{
        AudioListener.volume = volumn;
		DontDestroyOnLoad(gameObject);
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		if (switchListInt == null) switchListInt = new int[50];

		if (Skill == null) Skill = LoadJson<SkillData>.LoadJsonFromFile("Skills");
	}
    public void save()
    {
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
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.SetString("LastScene", lastScene);
     //   PlayerPrefs.SetInt("Guard", 1);
        for(int i = 0; i< 30; i++)
        {

            PlayerPrefs.SetInt("switchListInt" + i, switchListInt[i]);
        }
        PlayerPrefs.SetFloat("volumn", volumn);
    }
    public void read()
    {
        maxHP = PlayerPrefs.GetInt("maxHP");
        maxMP= PlayerPrefs.GetInt("maxMP");
        HP= PlayerPrefs.GetInt("HP" );
        MP =PlayerPrefs.GetInt("MP" );
        ATK =PlayerPrefs.GetInt("ATK" );
        DEF= PlayerPrefs.GetInt("DEF");
        MAT= PlayerPrefs.GetInt("MAT" );
        MDF =PlayerPrefs.GetInt("MDF" );
        AGI= PlayerPrefs.GetInt("AGI");
        LUC= PlayerPrefs.GetInt("LUC" );
        level= PlayerPrefs.GetInt("level" );
        EXP= PlayerPrefs.GetInt("EXP" );
        Money= PlayerPrefs.GetInt("Money");
        for (int i = 0; i < 30; i++)
        {

            switchListInt[i]= PlayerPrefs.GetInt("switchListInt"+i);
        }
        volumn = PlayerPrefs.GetFloat("Volunm");
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
        AudioListener.volume = volumn;
    }
	public void PlayerUseSkill(byte id)
	{

	}
    
}
