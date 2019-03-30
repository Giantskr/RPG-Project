using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
	public static int level = 1;
	public static int maxHP = 450;
	public static int maxMP = 90;
	public static int HP = maxHP;
	public static int MP = maxMP;
	public static int EXP = 0;
	public static int EXPToNextLevel = 50;
	public static int ATK = 16;
	public static int DEF = 18;
	public static int MAT = 16;
	public static int MDF = 14;
	public static int AGI = 32;
	public static int LUC = 5;

	public static bool[] switchListBool = { false };
	public static int[] switchListInt = { 0 };

	//public static Player_Stats instance = null;
	//void Awake()
	//{
	//	DontDestroyOnLoad(gameObject);
	//	if (instance == null)
	//		instance = this;
	//	else if (instance != this)
	//		Destroy(gameObject);
	//}
	//public static Player_Stats GetInstance()
	//{
	//	return instance;
	//}

	void Start()
    {
        
    }

    void Update()
    {
        
    }
}
