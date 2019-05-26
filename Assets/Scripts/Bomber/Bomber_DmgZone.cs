using UnityEngine;

public class Bomber_DmgZone : MonoBehaviour
{
    void FixedUpdate()
    {
		LayerMask mask = LayerMask.GetMask("Obstacle");
		Collider2D hit = Physics2D.OverlapPoint(transform.position, mask);
		if (hit)
		{
			switch (hit.tag)
			{
				case "Breakable": hit.GetComponent<Bomber_Brick>().DestroyBrick(true,transform.position); break;
				case "Bomb":hit.GetComponent<Bomber_Bomb>().Explode();break;
				case "Player":hit.GetComponent<Bomber_Player>().Die();break;
			}
		}
		
	}
}
