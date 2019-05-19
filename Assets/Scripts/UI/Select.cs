using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Select : MonoBehaviour
{
    public List<GameObject> Selections = new List<GameObject>();
    protected int states = 1;
    public int OptionNum;
 //   protected AudioSource audioSource;
	//public AudioClip[] UI_Sounds;

    void OnEnable()
	{
		OptionNum = Selections.Count;
		transform.position = Selections[0].transform.position;
	}

	protected virtual void Selection()
	{
		//audioSource = GetComponent<AudioSource>();
		if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))   //要用GetButtonDown而非GetButton，防止按键持续生效
		{
            //GetAxisRaw与GetKey(Down)对照：横坐标大于0为d，小于0为a；纵坐标大于0为w，小于0为s
            if ((Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Horizontal") > 0) && states < OptionNum) { states++; Input.ResetInputAxes(); }
            else if ((Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Horizontal") < 0) && states > 1) { states--; Input.ResetInputAxes(); } 
            GameManager.whichSound = 0;
        }
		else if (Input.GetButtonDown("Cancel")) GameManager.whichSound=3;
		else if (Input.GetButtonDown("Submit"))
		{
            //if(false) audioSource.PlayOneShot(UI_Sounds[2]);
            //else audioSource.PlayOneShot(UI_Sounds[1]);
            GameManager.whichSound = 1;
        }
		transform.localPosition = Selections[states - 1].transform.localPosition;
		//把绿条移动到选项文字上方
	}
}
