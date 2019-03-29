using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RPGTALK.Helper;

[RequireComponent(typeof(Rigidbody2D))]
public class Events : MonoBehaviour
{
	public Camera cam;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private RPGTalk rpgTalkHolder = null;
	[SerializeField]
	private RPGTalkCharacter temporaryNPC = null;
	[Tooltip("若交互物可旋转，则0下1左2上3右")]
	public Sprite[] idleSprites;
	[Space]
	public Sprite[] eventSprites;
	public AudioClip[] eventSounds;

	[Tooltip("角色朝向，仅供查看，任何情况下只能为Vector.down等四个方向的值")]
	public Vector2 faceOrientation = Vector2.down;
	protected Vector2 target;
	protected bool moving = false;

	protected Rigidbody2D rb;
	protected Animator an;

	void OnEnable()
	{
		rb = GetComponent<Rigidbody2D>();
		an = GetComponent<Animator>();
		target = rb.position;
		if (rpgTalkHolder != null)
		{
			rpgTalkHolder.OnNewTalk += OnNewTalk;
			rpgTalkHolder.OnEndTalk += OnEndTalk;
		}
	}

	public void OnCall(GameObject calling)
	{
		Events callingEvent = calling.GetComponent<Events>();
		switch (gameObject.name)
		{
			case "Passerby":
				SetFaceOrientation(-callingEvent.faceOrientation);
				CreateTemporaryNPC(temporaryNPC, "路人", eventSprites[0]);
				rpgTalkHolder.NewTalk("1", "2");
				break;
			case "TestBox":
				rpgTalkHolder.NewTalk("1", "1");
				break;
			default: break;
		}
	}

    void Update()
    {
        
    }

	void CreateTemporaryNPC(RPGTalkCharacter rpgTalkCharacter, string name, Sprite photo)
	{
		rpgTalkCharacter.dialoger = name;
		rpgTalkCharacter.photo = photo;
	}
	void OnNewTalk()
	{
		GameManager.inScene = false;
	}
	void OnEndTalk()
	{
		Input.ResetInputAxes();
		GameManager.inScene = true;
	}
	public void SetFaceOrientation(Vector2 vector)
	{
		faceOrientation = vector;
		SetSprite();
	}
	public void SetFaceOrientation(Vector2 pos, Vector2 vector)
	{
		transform.position = pos;
		faceOrientation = vector;
		SetSprite();
	}
	public void SetSprite()
	{
		if (!moving)
		{
			int x = 0;
			if (faceOrientation.x != 0) x = (int)faceOrientation.x + 1;
			GetComponent<SpriteRenderer>().sprite = idleSprites[x + (int)faceOrientation.y + 1];
		}
	}
	public void SetWalkAnimation(Vector2 vector)
	{
		//这里用不了switch，理由为Vector.right等不是常量
		if (vector == Vector2.right) an.Play("Right");
		else if (vector == Vector2.left) an.Play("Left");
		else if (vector == Vector2.up) an.Play("Up");
		else an.Play("Down");
	}
	protected bool FaceObstacle()
	{
		LayerMask mask = LayerMask.GetMask("Obstacle");
		LayerMask maskEdge = LayerMask.GetMask("Edge");
		if (Physics2D.Raycast(transform.position, faceOrientation, 1.1f, mask)) return true;
		else if (Physics2D.Raycast(transform.position, faceOrientation, 1.1f, maskEdge)) return true;
		else return false;
	}
	public void CallObject()
	{
		LayerMask mask = LayerMask.GetMask("Obstacle");
		RaycastHit2D hit = Physics2D.Raycast(transform.position, faceOrientation, 1.1f, mask);
		if (hit.collider != null && hit.collider.tag == "Accessible")
		{
			Input.ResetInputAxes();
			hit.collider.GetComponent<Events>().OnCall(gameObject);
		}
	}
	public void EndEvent()
	{
		Input.ResetInputAxes();
		GameManager.inScene = true;
	}
}
