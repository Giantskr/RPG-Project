using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : Events
{
    void Update()
    {
		if (GameManager.inScene)
		{
			MovePlayer();
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
			else if (Input.GetButton("Vertical") && !Input.GetButton("Horizontal")) 
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
		if (Vector2.Distance(rb.position, target) <= 0.01f)
		{
			moving = false;
			rb.position = target;
			an.enabled = false;
			SetSprite();
		}
	}
}
