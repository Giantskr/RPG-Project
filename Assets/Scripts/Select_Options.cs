using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Options : Select 
{
    public GameObject Options;
    public GameObject Menu;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localPosition = new Vector3(0, -77.5f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        selection();
        switch (states)
        {
            case 1:break;
            case 2:break;
            case 3:
                if (Input.GetButtonDown("Submit"))
                {
                    Menu.SetActive(true);
                    Options.SetActive(false);
                    audioSource.Play();
                }
                break;
        }
    }
}
