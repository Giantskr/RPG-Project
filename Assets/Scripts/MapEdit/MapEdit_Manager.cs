using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapEdit_Manager : GameManager
{
	public GameObject cam;
	public GameObject MapEdit_Esc;
	public static Vector2 SpawnPoint = new Vector2(-0.5f, 0.5f);

    void OnEnable()
    {
		fading = true;
		inScene = false;
		au = GetComponent<AudioSource>();
	}

    void Update()
    {
		SoundPlay();
		if (Input.GetButtonDown("Cancel") && inScene && !fading)
		{
			inScene = false;
			MapEdit_Esc.SetActive(true);
		}
	}
	public void EnterPlayMode()
	{
		player.GetComponent<SpriteRenderer>().color = Color.white;
		inScene = true;
	}
	public void ExitPlayMode()
	{
		Color color = Color.white;
		color.a = 0.5f;
		player.GetComponent<SpriteRenderer>().color = color;
		player.GetComponent<Events>().SetFaceOrientation(Vector2.down, SpawnPoint);
		cam.GetComponent<Rigidbody2D>().position = Vector2.zero;
	}
}
