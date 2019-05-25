using UnityEngine;

public class Bomber_Item : MonoBehaviour
{
	public enum ItemType
	{
		bomb, range, speed
	}
	public ItemType type;
	public Sprite[] itemSprites;
	[HideInInspector] public int spriteNum;

    void Start()
    {
		switch (spriteNum)
		{
			case 0: type = ItemType.bomb; break;
			case 1: type = ItemType.range; break;
			case 2: type = ItemType.speed; break;
		}
		GetComponent<SpriteRenderer>().sprite = itemSprites[spriteNum];
	}
}
