using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Purchase : Select
{
    public GameObject Select_Store;
    public GameObject goods;
    public static  bool isBought1 = false;
    public static  bool isBought2 = false;
    public static  bool isBought3 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Selection();
        if (Input.GetButtonDown("Cancel"))
        {
            Select_Store.SetActive(true);
            goods.SetActive(false);
        }
        if (Input.GetButtonDown("Submit"))
        {
            switch (states)
            {
                case 1:

                    isBought1 = PayMoney(0, 30, "小药瓶", "07", isBought1);                   
                    break;
                case 2:

                    isBought2=PayMoney(1, 50, "中药瓶", "08", isBought2);
                    break;
                case 3:
                    isBought3=PayMoney(2, 70, "大药瓶", "09", isBought3);
                    break;
            }
           
        }
        
    }
    public bool PayMoney(int i,int price,string name,string spritename,bool isbought)
    {
        if (Player_Stats.Money - price >= 0)
        {
            Player_Stats.Money -= price;
            if (!isbought)
            {
                Object_WeaponBag.Props.Add(new Item(name, Resources.Load<Sprite>(spritename), 1));
                Object_WeaponBag.propsize += 1;
                isbought = true;
            }
            else
            {
                Object_WeaponBag.Props[i].img = Resources.Load<Sprite>(spritename);
                Object_WeaponBag.Props[i].name = name;
                Object_WeaponBag.Props[i].num += 1;
            }
        }
        return isbought;
    }
}
