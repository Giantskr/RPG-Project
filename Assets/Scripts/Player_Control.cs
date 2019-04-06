using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGTALK.Helper;

public class Player_Control : Events
{
	Vector2 areaLastPos = Vector2.positiveInfinity;
	float animationSpeed;

    void FixedUpdate()
    {
		if (GameManager.inScene)
		{
			if (an.speed == 0) an.speed = animationSpeed;
			if (!GameManager.fading) MovePlayer();
			MoveCameraWithPlayer();
			if (!moving) CallObject();
		}
		else if (an.speed != 0) 
		{
			animationSpeed = an.speed;
			an.speed = 0;
		}
	}
	/// <summary>
	/// 玩家移动
	/// </summary>
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
				SetWalkAnimation();
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
	/// <summary>
	/// 使相机根据玩家位置移动
	/// </summary>
	void MoveCameraWithPlayer()
	{
		Vector2 camPos = rb.position;
		LayerMask mask = LayerMask.GetMask("Edge");
		RaycastHit2D hitRight = Physics2D.Raycast(camPos, Vector2.right, 8, mask);
		RaycastHit2D hitUp = Physics2D.Raycast(camPos, Vector2.up, 6, mask);
		if (hitRight) camPos.x = hitRight.point.x - 8;
		else
		{
			RaycastHit2D hitLeft = Physics2D.Raycast(camPos, Vector2.left, 8, mask);
			if (hitLeft) camPos.x = hitLeft.point.x + 8;
		}
		if (hitUp) camPos.y = hitUp.point.y - 6;
		else
		{
			RaycastHit2D hitDown = Physics2D.Raycast(camPos, Vector2.down, 6, mask);
			if (hitDown) camPos.y = hitDown.point.y + 6;
		} 
		cam.GetComponent<Rigidbody2D>().position = camPos;
	}
	/// <summary>
	/// 交互某个物体
	/// </summary>
	public void CallObject()
	{
		LayerMask mask = LayerMask.GetMask("Obstacle");
		if (Input.GetButtonDown("Submit"))
		{
			//RaycastHit2D hit = Physics2D.Raycast(transform.position, faceOrientation, 1.1f, mask);
			//if (hit && hit.collider.tag == "Accessible" && !hit.collider.isTrigger)
   //             hit.collider.GetComponent<Events>().OnCall(gameObject);
			RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, faceOrientation, 1.1f, mask);
			if (hit!=null)
			{
				for (int i = 0; i < hit.Length; i++)
				{
					if (hit[i] && hit[i].collider.tag == "Accessible" && !hit[i].collider.isTrigger)
						hit[i].collider.GetComponent<Events>().OnCall(gameObject);
				}
			}
        }
		else
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, faceOrientation, 0, mask);
			if (hit && hit.collider.tag == "Accessible" && hit.collider.isTrigger && (Vector2)hit.transform.position != areaLastPos)
			{
				areaLastPos = hit.transform.position;
				an.enabled = false;
				SetSprite();
				hit.collider.GetComponent<Events>().OnCall(gameObject);
			}
			else if (!hit) areaLastPos = Vector2.positiveInfinity;
		}
	}
}
