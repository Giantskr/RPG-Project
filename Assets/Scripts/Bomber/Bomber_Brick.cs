using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomber_Brick : MonoBehaviour
{
	public void DestroyBrick(bool self, Vector2 pos)
	{
		Vector3Int tilePos = GetComponent<Tilemap>().WorldToCell(pos);
		GetComponent<Tilemap>().SetTile(tilePos, null);
		if(self)
			FindObjectOfType<Socket_Client>().SendData("Item,x" + pos.x + ",y" + pos.y + ",Block,");
	}
}
