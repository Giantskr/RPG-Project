using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
	public Camera cam;
	[Tooltip("朝向sprite，0左1下2右3上")]
	public Sprite[] playerSprite;
	[Space(10)]
	Vector2 faceOrientation = Vector2.down;
	Vector2 target;
	bool moving = false;
	
	Rigidbody2D rb;
	Animator an;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		an = GetComponent<Animator>();
		target = transform.position;
    }

    void Update()
    {
		MovePlayer();
		
	}

	void MovePlayer()
	{
		if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && !moving) 
		{
			an.enabled = true;
			if (Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
			{
				if (Input.GetAxis("Horizontal") > 0) { faceOrientation = Vector2.right; an.Play("Right"); }
				else { faceOrientation = Vector2.left; an.Play("Left"); }
			}
			else if (Input.GetButton("Vertical") && !Input.GetButton("Horizontal"))
			{
				if (Input.GetAxis("Vertical") > 0) { faceOrientation = Vector2.up; an.Play("Up"); }
				else { faceOrientation = Vector2.down; an.Play("Down"); }
			}
			if (Input.GetButton("Run")) an.speed = 2f;
			else an.speed = 1f;
			if (!FaceObstacle())
			{
				moving = true;
				target = (Vector2)transform.position + faceOrientation;
			}
		}
		else if (moving) rb.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 4 * an.speed);
		if (rb.position == target)
		{
			moving = false;
			an.enabled = false;
			int y = 0;
			if (faceOrientation.y != 0) y = (int)faceOrientation.y + 1;
			GetComponent<SpriteRenderer>().sprite = playerSprite[(int)faceOrientation.x + y + 1];
		}
	}

	bool FaceObstacle()
	{
		RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + faceOrientation * 0.5f, faceOrientation, 0.5f);
		if (hit.collider != null && hit.collider.gameObject.layer == 8) return true;
		else return false;
	}

	public void SetFaceOrientation(Vector2 vector)
	{
		faceOrientation = vector;
	}
	public void SetFaceOrientation(Vector2 pos, Vector2 vector)
	{
		transform.position = pos;
		faceOrientation = vector;
	}
}
