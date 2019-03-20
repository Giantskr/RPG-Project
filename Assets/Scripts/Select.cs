using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Select : MonoBehaviour
{
    public List<GameObject> Selections = new List<GameObject>();
    protected  int states = 1;
    public int OptionNum;
    public AudioSource audioSource;

    void Start()
    {
        gameObject.transform.position = Selections[0].gameObject.transform.position;

    }
   
    protected void selection()
        {
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))   //要用GetButtonDown而非GetButton，防止按键持续生效
        {
            //GetAxis与GetKey(Down)对照：横坐标大于0为d，小于0为a；纵坐标大于0为w，小于0为s
            if ((Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") > 0) && states < OptionNum)
            {
                states += 1; audioSource.Play();
            }
            if ((Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") < 0) && states > 1) 
            {
                states -= 1; audioSource.Play();
            }
        }
        gameObject.transform.localPosition = Selections[states-1].gameObject.transform.localPosition;
        //把绿条移动到选项文字上方   
    }
    
}
