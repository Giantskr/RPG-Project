using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Purchase : Select
{
    public GameObject Select_Store;
    public GameObject goods;
    public Text Describe;
    public static bool[] isBought;
    //public static  bool isBought2 = false;
    //public static  bool isBought3 = false;
    //public static bool isBought4 = false;
    //public static bool isBought5 = false;
    //public static bool isBought6 = false;
    // Start is called before the first frame update
    void Start()
    {
        isBought = new bool[6];
    }

    // Update is called once per frame
    void Update()
    {
        Selection();
        switch (states)
        {
            case 1:
                Describe.text = "小瓶生命药剂 恢复30点生命值 价格 30G";
                break;
            case 2:
                Describe.text = "中瓶生命药剂 恢复50点生命值 价格 50G";
                break;
            case 3:
                Describe.text = "大瓶生命药剂 恢复70点生命值 价格 70G";
                break;
            case 4:
                Describe.text = "小瓶魔力药剂 恢复30点魔法值 价格 40G";
                break;
            case 5:
                Describe.text = "中瓶魔力药剂 恢复30点魔法值 价格 60G";
                break;
            case 6:
                Describe.text = "大瓶魔力药剂 恢复30点魔法值 价格 80G";
                break;
        }
        if (Input.GetButtonDown("Cancel"))
        {
            Describe.text = null;
           Select_Store.SetActive(true);
            goods.SetActive(false);
        }
        if (Input.GetButtonDown("Submit"))
        {
            switch (states)
            {
                case 1:

                    isBought[0] = PayMoney(0, 30, "小瓶生命药剂", "07", isBought[0]);                   
                    break;
                case 2:

                    isBought[1] = PayMoney(1, 50, "中瓶生命药剂", "08", isBought[1]);
                    break;
                case 3:
                    isBought[2] = PayMoney(2, 70, "大瓶生命药剂", "09", isBought[2]);
                    break;
                case 4:
                    isBought[3] = PayMoney(3, 40, "小瓶魔力药剂", "12", isBought[3]);
                    break;
                case 5:
                    isBought[4] = PayMoney(4, 60, "中瓶魔力药剂", "13", isBought[4]);
                    break;
                case 6:
                    isBought[5] = PayMoney(5, 80, "大瓶魔力药剂", "14", isBought[5]);
                    break;
            }
           
        }
        
    }
    public bool PayMoney(int i,int price,string name,string spritename,bool isbought)
    {
        if (Player_Stats.money - price >= 0)
        {
            Player_Stats.money -= price;
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
