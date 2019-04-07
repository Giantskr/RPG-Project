using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterOpenAni : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    public void LoadFirstScene()
    {
        gameManager.StartCoroutine("ChangeScene", "PalaceOut"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
