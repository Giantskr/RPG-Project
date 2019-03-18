using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Select : MonoBehaviour
{
    private int states = 1;
    public int OptionNum;
    public AudioSource audioSource;
    abstract public void Function1();
    abstract public void Function2();
    abstract public void Function3();
    abstract public void Function4();
    abstract public void Function5();
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.localPosition = new Vector3(0, -77.5f, 0);
    }

    // Update is called once per frame
   
   protected void selection()
        {
            if (Input.GetKeyDown("s") && states < OptionNum)
            {
                states += 1; audioSource.Play();
            }
            if (Input.GetKeyDown("w") && states > 1)
            {
                states -= 1; audioSource.Play();
            }
            switch (states)
            {
                case 1: this.gameObject.transform.localPosition = new Vector3(0, -77.5f, 0);Function1(); break;
                case 2: this.gameObject.transform.localPosition = new Vector3(0, -116.3f, 0);Function2();  break;
                case 3: this.gameObject.transform.localPosition = new Vector3(0, -155, 0);Function3(); break;
                case 4: this.gameObject.transform.localPosition = new Vector3(0, -155, 0); Function3(); break;
                case 5: this.gameObject.transform.localPosition = new Vector3(0, -155, 0); Function3(); break;

        }
    }
    
}
