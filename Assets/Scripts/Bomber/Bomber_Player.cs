using UnityEngine;

public class Bomber_Player : Player_Control
{
	public GameObject bomberManager;
	Socket_Client socket;

	int bombMaxCount = 1;
	int bombCount = 1;
	[HideInInspector]
	public int bombRange = 1;
	float speed = 1;
	public bool self;

	void OnEnable()
	{
		socket = FindObjectOfType<Socket_Client>();
		rb = GetComponent<Rigidbody2D>();
		au = GetComponent<AudioSource>();
		an = GetComponent<Animator>();
		target = rb.position;
	}

	void FixedUpdate()
    {
		if (self)
		{
			if (GameManager.inScene)
			{
				bombCount = bombMaxCount - bomberManager.GetComponent<Bomber_Manager>().playerBombParent.childCount;
				if (an.speed == 0) an.speed = animationSpeed;
				if (!GameManager.fading && Bomber_Manager.playerAlive && !Bomber_Manager.chatting) Control();
			}
			else if (an.speed != 0)
			{
				animationSpeed = an.speed;
				an.speed = 0;
			}
		}
		else
		{
			if (GameManager.inScene)
			{
				if (an.speed == 0) an.speed = animationSpeed;
				if (Bomber_Manager.enemyAlive) EnemyMove();
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
		if (!Input.GetKeyDown(KeyCode.Return) && Input.GetButtonDown("Submit") && bombCount > 0) 
		{
			Vector2 bombPos = target;
			if (moving) bombPos = target - faceOrientation;
			bomberManager.GetComponent<Bomber_Manager>().PlaceBomb(bombPos, bombRange, self);
			socket.SendData("Action,PlaceBomb,");
		}
		if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !moving)
		{
			if (Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
			{
				if (Input.GetAxisRaw("Horizontal") > 0)
				{
					faceOrientation = Vector2.right;
					socket.SendData("Action,Moveright,");
				}
				else 
				{
					faceOrientation = Vector2.left;
					socket.SendData("Action,Moveleft,");
				}
			}
			if (Input.GetButton("Vertical") && !Input.GetButton("Horizontal"))
			{
				if (Input.GetAxisRaw("Vertical") > 0)
				{
					faceOrientation = Vector2.up;
					socket.SendData("Action,Moveup,");
				}
				else
				{
					faceOrientation = Vector2.down;
					socket.SendData("Action,Movedown,");
				}
			}
			if (!FaceObstacle())
			{
				an.enabled = true;
				moving = true;
				target = rb.position + faceOrientation;
				SetWalkAnimation();
			}
		}
		if (moving)
		{
			rb.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 5 * an.speed * speed);
		}
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

	public void EnemyMove()
	{
		if (moving)
		{
			rb.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 5 * an.speed * speed);
		}
		if (Vector2.Distance(rb.position, target) <= 0.001f)
		{
			moving = false;
			rb.position = target;
			//if ((!Input.GetButton("Horizontal") && !Input.GetButton("Vertical")) || FaceObstacle())
			{
				an.enabled = false;
				SetSprite();
			}
		}
	}

	public void EnemyControl(string action)
	{
		if (action == "PlaceBomb")
		{
			Vector2 bombPos = target;
			if (moving) bombPos = target - faceOrientation;
			bomberManager.GetComponent<Bomber_Manager>().PlaceBomb(bombPos, bombRange, self);
		}
		else if (!moving) 
		{
			switch (action)
			{
				case "Moveright": faceOrientation = Vector2.right; break;
				case "Moveleft": faceOrientation = Vector2.left; break;
				case "Moveup": faceOrientation = Vector2.up; break;
				case "Movedown": faceOrientation = Vector2.down; break;
			}
			if (!FaceObstacle())
			{
				an.enabled = true;
				moving = true;
				target = rb.position + faceOrientation;
				SetWalkAnimation();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (Bomber_Manager.playerAlive || Bomber_Manager.enemyAlive) 
			if (collision.tag == "Item") GetItem(collision.gameObject);
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
		socket.SendData("Item," + item.transform.position.x + "," + item.transform.position.y + ",Destroy,");
		Destroy(item);
	}

	public void Die()
	{
		if (self) Bomber_Manager.playerAlive = false;
		else Bomber_Manager.enemyAlive = false;
		an.enabled = true;
		an.Play("Death");
	}
}
