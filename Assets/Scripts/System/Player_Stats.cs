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

	public static string lastScene;

	public static bool[] switchListBool;
	public static int[] switchListInt = { 0 };

	public static Player_Stats instance = null;

	SkillData Skill;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);




		if (switchListBool == null) switchListBool = new bool[50];
		if (switchListInt == null) switchListInt = new int[20];

		if (Skill == null) Skill = LoadJson<SkillData>.LoadJsonFromFile("Skills");
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

	}

    public void SetSwitchBool(int index, bool set)
    {
        switchListBool[index] = set;
    }
    public bool GetSwitchBool(int index)
    {
        return switchListBool[index];
    }
}
