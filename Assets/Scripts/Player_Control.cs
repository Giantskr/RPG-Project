using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : Events
{
	
	
    void Start()
    {
		
    }

    void Update()
    {
		if (GameManager.inScene)
		{
			MovePlayer();
			if (Input.GetButtonDown("Submit")) CallObject();
		}
	}

	void MovePlayer()
	{
		if (Input.GetButton("Run")) an.speed = 2f;
		else an.speed = 1f;
		if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !moving) 
		{
			float inputX = System.Math.Abs(Input.GetAxis("Horizontal"));
			float inputY = System.Math.Abs(Input.GetAxis("Vertical"));
			if (inputX > inputY) 
			{
				if (Input.GetAxisRaw("Horizontal") > 0) faceOrientation = Vector2.right;
				else faceOrientation = Vector2.left; 
			}
			else
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
		if (Vector2.Distance(rb.position,target)<=0.01f)
		{
			moving = false;
			rb.position = target;
			an.enabled = false;
			SetSprite();
		}
	}
}
