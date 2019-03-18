using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Options : Select 
{
    public GameObject Options;
    public GameObject Menu;
    public override void Function1()
    {
        
    }

    public override void Function2()
    {
    }

    public override void Function3()
    {
        if (Input.GetKeyDown("space"))
        {
            Menu.SetActive(true);
            Options.SetActive(false );
            audioSource.Play();
        }
    }

    public override void Function4()
    {
    }

    public override void Function5()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.localPosition = new Vector3(0, -77.5f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        selection();
    }
}
