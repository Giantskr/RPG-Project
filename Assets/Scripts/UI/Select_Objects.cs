using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Objects : Select
{
    public GameObject UI_Selections;
    public GameObject UI_Objects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        selection();
        if (Input.GetButtonDown("Cancel"))
        {
            UI_Selections.SetActive(true);
            UI_Objects.SetActive(false);
        }
        switch (states)
        {
            case 1:break;
        }
    }
}
