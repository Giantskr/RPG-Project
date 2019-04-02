using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Select_Sell : Select
{
    public GameObject Select_Store;
    public GameObject objects;
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
        if (Input.GetButtonDown("Cancel"))
        {
            Select_Store.SetActive(true);
            objects.SetActive(false);
        }
        if (Input.GetButtonDown("Submit"))
        {
            SellObjects(states-1);
        }

    }
    public void SellObjects(int states)
    {
        if(Object_WeaponBag.Props[states].num - 1 > 0)
        {
            Object_WeaponBag.Props[states].num -= 1;
            switch (Object_WeaponBag.Props[states].name)
            {
                case "小药瓶": Player_Stats.Money += 15; break;
                case "中药瓶": Player_Stats.Money += 25; break;
                case "大药瓶": Player_Stats.Money += 35; break;
            }
        }
        else
        {
            if (Object_WeaponBag.Props[states].num == 1)
            {
                switch (Object_WeaponBag.Props[states].name)
                {
                    case "小药瓶": Player_Stats.Money += 15; break;
                    case "中药瓶": Player_Stats.Money += 25; break;
                    case "大药瓶": Player_Stats.Money += 35; break;
                }
            }
            //Object_WeaponBag.Props[states].img= Resources.Load<Sprite>("00");
            //Object_WeaponBag.Props[states].name =null;
            Object_WeaponBag.Props[states].num = 0;
        }
    }
}
