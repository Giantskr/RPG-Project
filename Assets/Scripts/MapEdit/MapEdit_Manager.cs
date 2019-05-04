using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdit_Manager : GameManager
{
	public GameObject cam;
	public GameObject mapEdit_Esc;
	public static Vector2 spawnPoint = new Vector2(-0.5f, 0.5f);
	public static Vector2 tileSetPos = new Vector2(-0.5f, 0.5f);
	public static int holdingTile = 0;

	void OnEnable()
    {
		fading = true;
		inScene = false;
		spawnPoint = new Vector2(-0.5f, 0.5f);
		tileSetPos = new Vector2(-0.5f, 0.5f);
		holdingTile = 0;
		au = GetComponent<AudioSource>();
	}

    void Update()
    {
		SoundPlay();
		if (Input.GetButtonDown("Cancel") && inScene && !fading)
		{
			whichSound = 1;
			inScene = false;
			mapEdit_Esc.SetActive(true);
		}
	}

	public void EnterPlayMode()
	{
		player.GetComponent<SpriteRenderer>().color = Color.white;
		player.GetComponent<Events>().SetFaceOrientation(Vector2.down, spawnPoint);
		inScene = true;
	}

	public void ExitPlayMode()
	{
		Color color = Color.white;
		color.a = 0.5f;
		player.GetComponent<SpriteRenderer>().color = color;
		player.GetComponent<Events>().SetFaceOrientation(Vector2.down, spawnPoint);
		tileSetPos = new Vector2(-0.5f, 0.5f);
		cam.GetComponent<Rigidbody2D>().position = new Vector2(-0.5f, 0.5f);
	}
}
