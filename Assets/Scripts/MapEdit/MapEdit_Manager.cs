using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapEdit_Manager : GameManager
{
	

    void OnEnable()
    {
		fading = true;
		inScene = false;
		au = GetComponent<AudioSource>();
	}

    void Update()
    {
		SoundPlay();
	}
}
