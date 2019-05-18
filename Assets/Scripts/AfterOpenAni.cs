using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterOpenAni : MonoBehaviour
{
    public GameManager gameManager;

    public void LoadFirstScene()
    {
        gameManager.StartCoroutine("ChangeScene", "PalaceOut"); 
    }
}
