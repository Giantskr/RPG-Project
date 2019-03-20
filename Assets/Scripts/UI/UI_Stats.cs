using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Stats : MonoBehaviour
{
    public GameObject UI_Selections;
    public GameObject UI_State;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            UI_Selections.SetActive(true);
            UI_State.SetActive(false);
        }
    }
}
