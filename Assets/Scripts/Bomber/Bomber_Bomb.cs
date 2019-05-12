using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber_Bomb : MonoBehaviour
{
	float timeToExplode = 4f;
	float explodeDuration = 0.6f;
	public int range = 1;
	bool exploded = false;
	GameObject damageZone;

	void OnEnable()
    {
		damageZone = transform.GetChild(0).gameObject;
    }

    void Update()
    {
		if (!exploded)
		{
			timeToExplode -= Time.deltaTime;
			if (timeToExplode > 0.9f && timeToExplode < 1) GetComponent<Animator>().Play("GoingToExplode");
			if (timeToExplode <= 0) Explode();
		}
		else
		{
			explodeDuration -= Time.deltaTime;
			if (explodeDuration <= 0) Destroy(gameObject);
		}
    }

	public void Explode()
	{
		exploded = true;
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		gameObject.GetComponent<Collider2D>().enabled = false;
		damageZone.SetActive(true);
		bool[] accessable = new bool[4];
		for (int j = 0; j < 4; j++) accessable[j] = true;
		for (int i = 1; i <= range; i++)
		{
			LayerMask mask = LayerMask.GetMask("Obstacle");
			Collider2D hit;
			if (accessable[0])
			{
				hit = Physics2D.OverlapPoint(transform.position + Vector3.right * i, mask);
				if (hit)
				{
					if (hit.tag != "Bomb") accessable[0] = false;
					if (hit.tag != "Unbreakable") 
						Instantiate(damageZone, transform.position + Vector3.right * i, Quaternion.identity, transform);
				}
				else Instantiate(damageZone, transform.position + Vector3.right * i, Quaternion.identity, transform);
			}
			if (accessable[1])
			{
				hit = Physics2D.OverlapPoint(transform.position + Vector3.left * i, mask);
				if (hit)
				{
					if (hit.tag != "Bomb") accessable[1] = false;
					if (hit.tag != "Unbreakable")
						Instantiate(damageZone, transform.position + Vector3.left * i, Quaternion.identity, transform);
				}
				else Instantiate(damageZone, transform.position + Vector3.left * i, Quaternion.identity, transform);
			}
			if (accessable[2])
			{
				hit = Physics2D.OverlapPoint(transform.position + Vector3.up * i, mask);
				if (hit)
				{
					if (hit.tag != "Bomb") accessable[2] = false;
					if (hit.tag != "Unbreakable")
						Instantiate(damageZone, transform.position + Vector3.up * i, Quaternion.identity, transform);
				}
				else Instantiate(damageZone, transform.position + Vector3.up * i, Quaternion.identity, transform);
			}
			if (accessable[3])
			{
				hit = Physics2D.OverlapPoint(transform.position + Vector3.down * i, mask);
				if (hit)
				{
					if (hit.tag != "Bomb") accessable[3] = false;
					if (hit.tag != "Unbreakable")
						Instantiate(damageZone, transform.position + Vector3.down * i, Quaternion.identity, transform);
				}
				else Instantiate(damageZone, transform.position + Vector3.down * i, Quaternion.identity, transform);
			}
		}
	}
}
