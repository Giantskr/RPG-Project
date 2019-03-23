using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject UI_Esc;
	public static bool inScene;

	public static GameManager instance = null;
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}
	public static GameManager GetInstance()
	{
		return instance;
	}

	void OnEnable()
    {
		inScene = true;
    }

    void Update()
    {
		if (Input.GetButtonDown("Cancel") && inScene) 
		{
			inScene = false;
			UI_Esc.SetActive(true);
		}
    }
}
