using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomber_Brick : MonoBehaviour
{
	public void DestroyBrick(Vector2 pos)
	{
		Vector3Int tilePos = GetComponent<Tilemap>().WorldToCell(pos);
		GetComponent<Tilemap>().SetTile(tilePos, null);
		FindObjectOfType<Socket_Client>().SendData("Item,x" + pos.x + ",y" + pos.y + ",Block,");
	}
}
