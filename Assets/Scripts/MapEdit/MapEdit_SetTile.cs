using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEdit_SetTile : MonoBehaviour
{
	public enum TileMode
	{
		Tile, PlayerSpawn
	}
	public static TileMode tileMode;

	public Camera cam;
	public GameObject tileWindow;
	public Transform playerSpawn;
	public Sprite spawnSprite;
	public Tilemap map;
	public Tile[] tile;
	LayerMask mask;


	void OnEnable()
    {
		mask = LayerMask.GetMask("Edge");
		transform.position = MapEdit_Manager.tileSetPos;
	}

    void Update()
    {
		if(tileMode == TileMode.Tile)
			transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = tile[MapEdit_Manager.holdingTile].sprite;
		else transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spawnSprite;
		if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
		{
			MoveArea();
			MoveCameraWithArea();
		}
		else if (Input.GetButtonDown("Submit"))
		{
			GameManager.whichSound = 1;
			if (tileMode == TileMode.Tile) SetTileInMap();
			else SetPlayerSpawn();
		}
		else if (Input.GetButtonDown("Cancel"))
		{
			Input.ResetInputAxes();
			GameManager.whichSound = 3;
			tileWindow.SetActive(true);
			gameObject.SetActive(false);
		}
	}

	void MoveArea()
	{
		if (Input.GetAxisRaw("Horizontal") > 0 && !Input.GetButtonDown("Vertical"))
		{
			if(!Physics2D.Raycast(transform.position, Vector2.right, 1, mask)) MapEdit_Manager.tileSetPos += Vector2.right;
			else GameManager.whichSound = 0;
		}
		if (Input.GetAxisRaw("Horizontal") < 0 && !Input.GetButtonDown("Vertical"))
		{
			if(!Physics2D.Raycast(transform.position, Vector2.left, 1, mask)) MapEdit_Manager.tileSetPos += Vector2.left;
			else GameManager.whichSound = 0;
		}
		if (Input.GetAxisRaw("Vertical") > 0 && !Input.GetButtonDown("Horizontal"))
		{
			if(!Physics2D.Raycast(transform.position, Vector2.up, 1, mask)) MapEdit_Manager.tileSetPos += Vector2.up;
			else GameManager.whichSound = 0;
		}
		if (Input.GetAxisRaw("Vertical") < 0 && !Input.GetButtonDown("Horizontal"))
		{
			if(!Physics2D.Raycast(transform.position, Vector2.down, 1, mask)) MapEdit_Manager.tileSetPos += Vector2.down;
			else GameManager.whichSound = 0;
		}
		if((Vector2)transform.position != MapEdit_Manager.tileSetPos)
		{
			transform.position = MapEdit_Manager.tileSetPos;
			GameManager.whichSound = 0;
		}
	}

	void MoveCameraWithArea()
	{
		Vector2 camPos = MapEdit_Manager.tileSetPos;
		RaycastHit2D hitRight = Physics2D.Raycast(camPos, Vector2.right, 8, mask);
		RaycastHit2D hitUp = Physics2D.Raycast(camPos, Vector2.up, 6, mask);
		if (hitRight) camPos.x = hitRight.point.x - 8;
		else
		{
			RaycastHit2D hitLeft = Physics2D.Raycast(camPos, Vector2.left, 8, mask);
			if (hitLeft) camPos.x = hitLeft.point.x + 8;
		}
		if (hitUp) camPos.y = hitUp.point.y - 6;
		else
		{
			RaycastHit2D hitDown = Physics2D.Raycast(camPos, Vector2.down, 6, mask);
			if (hitDown) camPos.y = hitDown.point.y + 6;
		}
		cam.GetComponent<Rigidbody2D>().position = camPos;
	}

	void SetTileInMap()
	{
		Vector3Int pos = map.WorldToCell(transform.position);
		map.SetTile(pos, tile[MapEdit_Manager.holdingTile]);
	}

	void SetPlayerSpawn()
	{
		playerSpawn.position = transform.position;
		MapEdit_Manager.spawnPoint = transform.position;
	}
}
