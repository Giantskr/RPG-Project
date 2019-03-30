using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject cam;
	public GameObject UI_Esc;
	public GameObject fadingScreen;
	public static bool inScene;	
	public static bool fading;	


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
		fading = true;
		inScene = true;
    }

    void Update()
    {
		if (Input.GetButtonDown("Cancel") && inScene && !fading) 
		{
			inScene = false;
			UI_Esc.SetActive(true);
		}
    }

	
}
