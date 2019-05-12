using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber_Player : Player_Control
{
	public GameObject bomberManager;

	int bombMaxCount = 1;
	int bombCount = 1;
	int bombRange = 1;
	float speed = 1;
	public bool self;

    void FixedUpdate()
    {
		if (self)
		{
			if (GameManager.inScene)
			{
				bombCount = bombMaxCount - bomberManager.GetComponent<Bomber_Manager>().playerBombParent.childCount;
				if (an.speed == 0) an.speed = animationSpeed;
				if (!GameManager.fading && Bomber_Manager.playerAlive) Control();
			}
			else if (an.speed != 0)
			{
				animationSpeed = an.speed;
				an.speed = 0;
			}
		}	
	}
	protected override bool FaceObstacle()
	{
		LayerMask mask = LayerMask.GetMask("Obstacle");
		RaycastHit2D[] hit = Physics2D.RaycastAll((Vector2)transform.position + faceOrientation * 0.5f, faceOrientation, 0.9f, mask);
		LayerMask maskEdge = LayerMask.GetMask("Edge");
		RaycastHit2D hitEdge = Physics2D.Raycast(transform.position, faceOrientation, 1.1f, maskEdge);
		if (hit != null || hitEdge)
		{
			for (int i = 0; i < hit.Length; i++)
				if (hit[i] && !hit[i].collider.isTrigger)
				{
					if (hit[i].collider.tag == "Player") return false;
					else return true;
				}
			if (hitEdge && !hitEdge.collider.isTrigger) return true;
		}
		return false;
	}
	void Control()
	{
		if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !moving)
		{
			if (Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
			{
				if (Input.GetAxisRaw("Horizontal") > 0) faceOrientation = Vector2.right;
				else faceOrientation = Vector2.left;
			}
			if (Input.GetButton("Vertical") && !Input.GetButton("Horizontal"))
			{
				if (Input.GetAxisRaw("Vertical") > 0) faceOrientation = Vector2.up;
				else faceOrientation = Vector2.down;
			}
			if (!FaceObstacle())
			{
				an.enabled = true;
				moving = true;
				target = rb.position + faceOrientation;
				SetWalkAnimation();
			}
		}
		if (moving) rb.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 5 * an.speed * speed);
		if (Vector2.Distance(rb.position, target) <= 0.001f)
		{
			moving = false;
			rb.position = target;
			if ((!Input.GetButton("Horizontal") && !Input.GetButton("Vertical")) || FaceObstacle())
			{
				an.enabled = false;
				SetSprite();
			}
		}
		if (Input.GetButtonDown("Submit") && bombCount > 0)
		{
			Vector2 bombPos = target;
			if (moving) bombPos = target - faceOrientation;
			bomberManager.GetComponent<Bomber_Manager>().PlaceBomb(bombPos, bombRange, self);
		}
	}
	void GetItem(GameObject item)
	{
		Bomber_Item _Item = item.GetComponent<Bomber_Item>();
		switch (_Item.type)
		{
			case Bomber_Item.ItemType.bomb: if (bombMaxCount < 8) bombMaxCount++; break;
			case Bomber_Item.ItemType.range: if (bombRange < 8) bombRange++; break;
			case Bomber_Item.ItemType.speed: if (speed < 2) speed +=0.2f; break;
		}
		Destroy(item);
	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (Bomber_Manager.playerAlive)
			if (collision.tag == "Item") GetItem(collision.gameObject);
	}
	public void Die()
	{
		if (self) Bomber_Manager.playerAlive = false;
		else Bomber_Manager.enemyAlive = false;
		an.enabled = true;
		an.Play("Death");
	}
}
