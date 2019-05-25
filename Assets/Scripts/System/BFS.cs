using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BFS : MonoBehaviour
{
	public Tilemap obstacle;
	public Vector2 startPos;
	public Vector2 endPos;
    int n, m;
    int[][] map;

    void Start()
    {
		map = new int[(int)(endPos.y - startPos.y + 1)][];
		for (float y = startPos.y; y <= endPos.y; y++) 
		{
			int i = (int)(y - startPos.y);
			for(float x = startPos.x; x<= endPos.x; x++)
			{
				int j = (int)(x - startPos.x);
				Vector3Int cellPosition = obstacle.WorldToCell(new Vector2(x, y));
				if (obstacle.HasTile(cellPosition)) map[i][j] = 1;
				else map[i][j] = 0;
			}
		}
    }
}
