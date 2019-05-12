using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Select_Forge :Select
{
    public GameObject SelectForge;
    public GameObject objects;
    public Text Describe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Selection();
        for (int i = 0; i < Object_WeaponBag.propsize; i++)
        {
            Selections[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Props[i].img;
            Selections[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Props[i].name + "X" + Object_WeaponBag.Props[i].num;
        }

    }
}
