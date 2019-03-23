using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Stats : MonoBehaviour
{
    public GameObject UI_Selections;
    public GameObject UI_State;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            UI_Selections.SetActive(true);
            UI_State.SetActive(false);
        }
    }
}
