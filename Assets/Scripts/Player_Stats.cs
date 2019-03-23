using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
	public int HP;
	public int MP;
	public int EXP;
	public int ATK;
	public int DEF;
	public int MAT;
	public int MDF;
	public int AGI;
	public int LUC;

	public static Player_Stats instance = null;
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}
	public static Player_Stats GetInstance()
	{
		return instance;
	}

	void Start()
    {
        
    }


    void Update()
    {
        
    }
}
