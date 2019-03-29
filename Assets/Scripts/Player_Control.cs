using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : Events
{
	
    void FixedUpdate()
    {
		if (GameManager.inScene)
		{
			MovePlayer();
			MoveCameraWithPlayer();
			if (!moving && Input.GetButtonDown("Submit")) CallObject();
		}
	}

	void MovePlayer()
	{
		if (Input.GetButton("Run")) an.speed = 2f;
		else an.speed = 1f;
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
				SetWalkAnimation(faceOrientation);
			}
		}
		if (moving) rb.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 5 * an.speed);
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
	}
	void MoveCameraWithPlayer()
	{
		Vector2 camPos = rb.position;
		LayerMask mask = LayerMask.GetMask("Edge");
		RaycastHit2D[] hit = Physics2D.BoxCastAll(rb.position, new Vector2(16, 12), 0, Vector2.zero, 0, mask);
		for (int i = 0; i < hit.Length; i++)
		{
			if (hit[i].transform.name.StartsWith("H")) camPos.x = cam.GetComponent<Rigidbody2D>().position.x;
			else if (hit[i].transform.name.StartsWith("V")) camPos.y = cam.GetComponent<Rigidbody2D>().position.y;
		}
		cam.GetComponent<Rigidbody2D>().position = camPos;
	}
}
