using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEdit_Select_Tile : Select
{
	public GameObject editMode;
	public GameObject tileWindow;
	public GameObject tileSetArea;
	public GameObject mapEditManager;

    void OnEnable()
    {
		states = 2;
    }

    void Update()
    {
		AudioListener.volume = Player_Stats.volume;
		if (!GameManager.fading)
		{
			GetComponent<Image>().enabled = true;
			Selection();
			if (Input.GetButtonDown("Submit"))
			{
				switch (states)
				{
					case 1:
						editMode.SetActive(true);
						break;
					case 2:
						mapEditManager.GetComponent<MapEdit_Manager>().EnterPlayMode();
						break;
					case 3:
						tileSetArea.SetActive(true);
						MapEdit_SetTile.tileMode = MapEdit_SetTile.TileMode.PlayerSpawn;
						break;
					default:
						tileSetArea.SetActive(true);
						MapEdit_SetTile.tileMode = MapEdit_SetTile.TileMode.Tile;
						break;
				}
				tileWindow.SetActive(false);
			}
			else if (Input.GetButtonDown("Cancel")) states = 1;
		}
		else GetComponent<Image>().enabled = false;
	}
}
