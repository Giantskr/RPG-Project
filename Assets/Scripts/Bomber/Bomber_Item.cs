using UnityEngine;

public class Bomber_Item : MonoBehaviour
{
	public enum ItemType
	{
		bomb, range, speed
	}
	public ItemType type;
	public Sprite[] itemSprites;

    void OnEnable()
    {
		int rand = Random.Range(0, 100);
		int spriteNum = 0;
		if (rand <= 35) type = ItemType.bomb;
		else if (rand <= 70) { type = ItemType.range; spriteNum = 1; }
		else { type = ItemType.speed; spriteNum = 2; }
		GetComponent<SpriteRenderer>().sprite = itemSprites[spriteNum];
	}
}
