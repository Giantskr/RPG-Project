using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Object_WeaponBag>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
