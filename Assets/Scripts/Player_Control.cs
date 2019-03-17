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
	bool faceObstacle=false;
	
	Rigidbody2D rb;
	Animator an;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		an = GetComponent<Animator>();
    }

    void Update()
    {
		RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + faceOrientation * 0.5f, faceOrientation, 0.1f);
		if (hit.collider != null && hit.collider.tag == "Accessible") faceObstacle = true;
		else faceObstacle = false;
		if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
		{
			an.enabled = true;
			if (Input.GetButton("Run"))
			{
				an.speed = 1.5f;
				rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * 6;
			}
			else
			{
				an.speed = 1f;
				rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * 4;
			}
			if (Input.GetButton("Horizontal") && !Input.GetButton("Vertical")) 
			{
				if (Input.GetAxis("Horizontal") > 0) { an.Play("Right"); faceOrientation = Vector2.right; }
				else { an.Play("Left"); faceOrientation = Vector2.left; }
			}
			else if(Input.GetButton("Vertical") && !Input.GetButton("Horizontal"))
			{
				if (Input.GetAxis("Vertical") > 0) { an.Play("Up"); faceOrientation = Vector2.up; }
				else { an.Play("Down"); faceOrientation = Vector2.down; }
			}
		}
		else
		{
			an.enabled = false;
			rb.velocity = Vector2.zero;
			int y=0;
			if (faceOrientation.y != 0) y = (int)faceOrientation.y + 1;
			GetComponent<SpriteRenderer>().sprite = playerSprite[(int)faceOrientation.x + y + 1];
		}
    }

	public void SetFaceOrientation(Vector2 vector)
	{
		faceOrientation = vector;
	}
}
