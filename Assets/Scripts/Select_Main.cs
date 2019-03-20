using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_Main : Select
{
    public GameObject Options;
    public GameObject Menu;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.localPosition = new Vector3(0, -77.5f, 0);
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
                    SceneManager.LoadScene("Town");
                }
                break;
            case 2: break;
            case 3:
                if (Input.GetButtonDown("Submit"))
                {
                    Options.SetActive(true);
                    Menu.SetActive(false);
                    audioSource.Play();
                }
                break;
        }
    }
}
