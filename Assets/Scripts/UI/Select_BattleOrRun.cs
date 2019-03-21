using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_BattleOrRun : Select
{
    public GameObject Battle;
    public GameObject BattleOrRun;
    public GameObject L;
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
                    L.SetActive(true);
                    BattleOrRun.SetActive(false);                    
                    audioSource.Play();
                }
                break;
            case 2:
                if (Input.GetButtonDown("Submit"))
                {
                    Battle.SetActive(false);
                    audioSource.Play();
                }
                break;
        }
    }
}
