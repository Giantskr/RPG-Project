using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Store : Select
{
    [Tooltip("放入的第一个obj为整个UI,其余为选项")]
    public GameObject[] elements;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Selection();
        if (Input.GetButtonDown("Cancel"))
        {
            elements[0].SetActive(false);
            GameManager.inScene = true;
        }
        if (Input.GetButtonDown("Submit"))
        {
            Debug.Log("?????????????");
            if(states == elements.Length)
            {
                elements[0].SetActive(false); GameManager.inScene = true;
            }
            else
            {
                elements[states].SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
