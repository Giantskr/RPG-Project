using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Esc :Select
{
    public GameObject UI_Objects;
    public GameObject UI_Skills;
    public GameObject UI_Weapons;
    public GameObject UI_States;
    public GameObject UI_Settings;
    public GameObject UI_Save;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        selection();
        switch (states)
        {
            case 1:
                if (Input.GetButtonDown("Submit"))
                {
                    UI_Objects.SetActive(true);
                    gameObject.SetActive(false);
                }
                     break;
            case 2: break;
            case 3: break;
            case 4: break;
            case 5: break;
            case 6: break;
            case 7: break;
        }
    }
}
