using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomber_Brick : MonoBehaviour
{
	float itemDropRate = 0.25f;
	public GameObject itemPrefab;

    void OnEnable()
    {
		
	}

    void Update()
    {

    }

	public void DestroyBrick(Vector2 pos)
	{
		Vector3Int tilePos = GetComponent<Tilemap>().WorldToCell(pos);
		GetComponent<Tilemap>().SetTile(tilePos, null);
		if (Random.Range(0f, 1f) <= itemDropRate) Instantiate(itemPrefab, pos, Quaternion.identity);
	}
}
