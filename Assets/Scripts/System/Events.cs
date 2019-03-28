using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Events : MonoBehaviour
{
	public Camera cam;
	public GameObject UI_Conversation;
	public GameObject player;
	[Tooltip("若交互物可旋转，则0下1左2上3右")]
	public Sprite[] idleSprites;
	[Space]
	public Sprite[] eventSprites;
	public AudioClip[] eventSounds;

	[Tooltip("角色朝向，任何情况下只能为Vector.down等四个方向的值")]
	public Vector2 faceOrientation = Vector2.down;
	protected Vector2 target;
	protected bool moving = false;

	string dialogText;

	protected Rigidbody2D rb;
	protected Animator an;

	void OnEnable()
	{
		rb = GetComponent<Rigidbody2D>();
		an = GetComponent<Animator>();
		target = rb.position;
	}

	public IEnumerator OnCall(GameObject calling)
	{
		Debug.Log("abbba");
		Events callingEvent = calling.GetComponent<Events>();
		switch (gameObject.name)
		{
			case "Passerby":
				GameManager.inScene = false;
				SetFaceOrientation(-callingEvent.faceOrientation);
				dialogText = "<color=orange>路人</color>\n我只是一个普通的路人。";
				Conversation(eventSprites[1], dialogText);
				if (Input.GetButtonDown("Submit"))
				{
					Debug.Log("aaa");
					EndEvent();
					yield return new WaitForEndOfFrame();
				}
				break;
			
			default: break;
		}
		
	}

    void Update()
    {
        
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
		int x = 0;
		if (faceOrientation.x != 0) x = (int)faceOrientation.x + 1;
		GetComponent<SpriteRenderer>().sprite = idleSprites[x + (int)faceOrientation.y + 1];
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
		RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + faceOrientation * 0.5f, faceOrientation, 0.5f);
		if (hit.collider != null && hit.collider.gameObject.layer == 8) return true;
		else return false;
	}
	public void CallObject()
	{
		RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + faceOrientation * 0.5f, faceOrientation, 0.5f);
		if (hit.collider != null && hit.collider.tag == "Accessible")
		{
			Input.ResetInputAxes();
			hit.collider.GetComponent<Events>().StartCoroutine("OnCall", gameObject);
		}
	}

	public void Conversation(string text)
	{
		UI_Conversation.SetActive(true);
		for (int i = 1; i <= 2; i++) UI_Conversation.transform.GetChild(i).gameObject.SetActive(true);
		for (int i = 3; i <= 4; i++) UI_Conversation.transform.GetChild(i).gameObject.SetActive(false);
		GameObject voiceOver = UI_Conversation.transform.GetChild(2).gameObject;
		voiceOver.GetComponent<Text>().text = text;
	}
	public void Conversation(Sprite avatarSprite, string text)
	{
		UI_Conversation.SetActive(true);
		UI_Conversation.transform.GetChild(1).gameObject.SetActive(true);
		UI_Conversation.transform.GetChild(2).gameObject.SetActive(false);
		for (int i = 3; i <= 4; i++) UI_Conversation.transform.GetChild(i).gameObject.SetActive(true);
		GameObject avatar = UI_Conversation.transform.GetChild(3).gameObject;
		GameObject dialog = UI_Conversation.transform.GetChild(4).gameObject;
		avatar.GetComponent<Image>().sprite = avatarSprite;
		dialog.GetComponent<Text>().text = text;
	}
	public int MakeDecision()
	{
		int decision=0;

		return decision;
	}
	public void EndEvent()
	{
		UI_Conversation.SetActive(false);
		GameManager.inScene = true;
	}
}
