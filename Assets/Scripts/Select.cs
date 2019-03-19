using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Select : MonoBehaviour
{
    public List<GameObject> Selections = new List<GameObject>();
    protected  int states = 1;
    public int OptionNum;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = Selections[0].gameObject.transform.position;

    }

    // Update is called once per frame
   
   protected void selection()
        {
        if (Input.GetButton("Vertical"))
        {
            if (Input.GetKeyDown("s") && states < OptionNum)
            {
                states += 1; audioSource.Play();
            }
            if (Input.GetKeyDown("w") && states > 1)
            {
                states -= 1; audioSource.Play();
            }
        }
        gameObject.transform.localPosition = Selections[states-1].gameObject.transform.localPosition;
        //把绿条移动到选项文字上方   
    }
    
}
