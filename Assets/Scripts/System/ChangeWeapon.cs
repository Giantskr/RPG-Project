using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
    public List<GameObject> weapon, head, body;
    public GameObject Laidweapon;
    public GameObject Laidhead;
    public GameObject Laidbody;
    public static int weaponchange = 0;
    public static int helmetchange = 0;
    public static int armorchange = 0;
    //0 for not equip;
    //1 for equipping;
    //2 for had equipped;
    //3 for reset;
    //4 for Change;
    //requests are received from Select_Equip.cs
    public static int weaponchangeWhich;
    public static int armorchangeWhich;
    public static int helmetchangeWhich;
    public static bool save = false;
    int i2 = 0;
    int tempDataW1, tempDataW2, tempDataW3, tempDataW4, tempDataW5;
    int tempDataA1, tempDataA2, tempDataA3, tempDataA4, tempDataA5;
    int tempDataH1, tempDataH2, tempDataH3, tempDataH4, tempDataH5;
    ArmorsData armors;
    WeaponsData weapons;
    HelmetsData helmets;

    private void Awake()
    {
        weaponchange = 0;
        helmetchange = 0;
        armorchange = 0;
        weaponchangeWhich = 0;
        armorchangeWhich = 0;
        helmetchangeWhich = 0;

    }
    void Start()
    {
        armors = LoadJson<ArmorsData>.LoadJsonFromFile("Armors");
        weapons = LoadJson<WeaponsData>.LoadJsonFromFile("Weapons");
        helmets = LoadJson<HelmetsData>.LoadJsonFromFile("Helmets");
        Invoke("Read", 0.1f);
    }
    public void Read()
    {
        int i1 = 0;
        int i2 = 0;
        int i3 = 0;
        foreach (var data in weapon)
        {
            Debug.Log(data.transform.GetChild(1).GetComponent<Text>().text);
            if (data.transform.GetChild(1).GetComponent<Text>().text == PlayerPrefs.GetString("PlacedWeapon"))
            {
                weaponchangeWhich = i1;
                weaponchange = 1; 
            }
            i1++;
        }
        foreach (var data in body)
        {
            Debug.Log(data.transform.GetChild(1).GetComponent<Text>().text);
            if (data.transform.GetChild(1).GetComponent<Text>().text == PlayerPrefs.GetString("PlacedArmor"))
            {
                armorchangeWhich = i2;
                armorchange = 1;
            }
            i2++;
        }
        foreach (var data in head)
        {
            Debug.Log(data.transform.GetChild(1).GetComponent<Text>().text);
            if (data.transform.GetChild(1).GetComponent<Text>().text == PlayerPrefs.GetString("PlacedHelmet"))
            {
                helmetchangeWhich = i3;
                helmetchange = 1;
            }
            i3++;
        }
    }
    void Update()
    {
        if (Laidweapon.transform.GetChild(1).GetComponent<Text>().text != "空")
        {
            foreach (var data in weapons.Weapons)
            {
                if (data.objectName == Laidweapon.transform.GetChild(1).GetComponent<Text>().text)
                {
                    tempDataW1 = data.ATK;
                    tempDataW2 =data.DEF;
                    tempDataW3 =data.MAT;
                    tempDataW4 =data.MDF;
                    tempDataW5= data.AGI;
                    //Player_Stats.maxHP = + data.maxHP;
                    //Player_Stats.maxMP = + data.maxMP;
                    //尚未找到解决方案

                }
            }
            
        }
       
        if (Laidhead.transform.GetChild(1).GetComponent<Text>().text != "空")
        {
            foreach (var data in helmets.Helmets)
            {
                if (data.objectName == Laidhead.transform.GetChild(1).GetComponent<Text>().text)
                {
                    tempDataH1 = data.ATK;
                    tempDataH2 = data.DEF;
                    tempDataH3 = data.MAT;
                    tempDataH4 = data.MDF;
                    tempDataH5 = data.AGI;

                }
            }
        }
        if (Laidbody.transform.GetChild(1).GetComponent<Text>().text != "空")
        {
            foreach (var data in armors.Armors)
            {
                if (data.objectName == Laidbody.transform.GetChild(1).GetComponent<Text>().text)
                {
                    tempDataA1 = data.ATK;
                    tempDataA2 = data.DEF;
                    tempDataA3 = data.MAT;
                    tempDataA4 = data.MDF;
                    tempDataA5 = data.AGI;
                }
            }
        }

        Player_Stats.ATK = 16 + tempDataW1 + tempDataH1 + tempDataA1;
        Player_Stats.DEF = 18 + tempDataW2 + tempDataH2 + tempDataA2;
        Player_Stats.MAT = 16 + tempDataW3 + tempDataH3 + tempDataA3;
        Player_Stats.MDF = 14 + tempDataW4 + tempDataH4 + tempDataA4;
        Player_Stats.AGI = 32 + tempDataW5 + tempDataH5 + tempDataA5;

        //    if (save)
        //    {
        //        SavePlacedObjs();
        //        weaponchange = 2;
        //        helmetchange = 2;
        //        armorchange = 2;
        ////0 for not equip;
        //save = false;
        //    }


        weaponchange = Equip(weaponchange, weaponchangeWhich, Object_WeaponBag.weaponsize, weapon, Object_WeaponBag.Weapons, Object_WeaponBag.Weapons[weaponchangeWhich].name, 30, Laidweapon);
            armorchange = Equip(armorchange, armorchangeWhich, Object_WeaponBag.armorsize, body, Object_WeaponBag.Armors, Object_WeaponBag.Armors[armorchangeWhich].name, 0, Laidbody);
            helmetchange = Equip(helmetchange, helmetchangeWhich, Object_WeaponBag.helmetsize, head, Object_WeaponBag.Helmets, Object_WeaponBag.Helmets[helmetchangeWhich].name, 0, Laidhead);
        if (Laidweapon.transform.GetChild(1).GetComponent<Text>().text != "空")
        {
            SavePlacedObjs();
            //Debug.Log(PlayerPrefs.GetString("PlacedWeapon"));
        }
    }
    protected int Equip(int change,int changeWhich,int size,List<GameObject> sorts ,List<Item> item,string name,int stastic,GameObject laid)
    {
        switch (change)
        {
            case 0:
                //未装备武器时，确保武器按 添加进背包的时间先后顺序 排列
                for (int i = 0; i < size; i++)
                {
                    sorts[i].transform.GetChild(0).GetComponent<Image>().sprite = item[i].img;
                    sorts[i].transform.GetChild(1).GetComponent<Text>().text = item[i].name;
                    //Debug.Log(sorts[i].name);
                }
                break;

            case 1://收到放入武器位的指令
                ChangeWeapons(laid,sorts,size,changeWhich,item);
                change = 2;
                break;

            case 3://收到武器放回的指令

                laid .transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
                laid .transform.GetChild(1).GetComponent<Text>().text = null;
                for (int i = 0; i < size; i++)
                {
                    sorts[i].transform.GetChild(0).GetComponent<Image>().sprite = item[i].img;
                    sorts [i].transform.GetChild(1).GetComponent<Text>().text =item[i].name;
                }
                change = 0;
                break;

            case 4://收到交换武器指令
                Sprite tempImage;
                string tempText;
                tempImage = laid .transform.GetChild(0).GetComponent<Image>().sprite;
                tempText = laid .transform.GetChild(1).GetComponent<Text>().text;
                laid .transform.GetChild(0).GetComponent<Image>().sprite = sorts [changeWhich].transform.GetChild(0).GetComponent<Image>().sprite;
                laid .transform.GetChild(1).GetComponent<Text>().text = sorts [changeWhich].transform.GetChild(1).GetComponent<Text>().text;
                sorts [changeWhich].transform.GetChild(0).GetComponent<Image>().sprite = tempImage;
                sorts [changeWhich].transform.GetChild(1).GetComponent<Text>().text = tempText;
                tempImage = null;
                tempText = null;
                change = 2;
                break;
        }
        return change;
    }
    public void ChangeWeapons(GameObject Laid,List< GameObject> Sort,int size,int ChangeWhich,List<Item> Item)
    {
        //图片，名字传入装备栏
        Laid.transform.GetChild(0).GetComponent<Image>().sprite = Item [ChangeWhich].img;
        Laid.transform.GetChild(1).GetComponent<Text>().text =Item [ChangeWhich].name;
        //整理将武器装备后 背包内剩余武器的排列
        for (int i = ChangeWhich; i < size; i++)
        {
            
            if (i ==size -1)
            {
                Sort [i].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
                Sort[i].transform.GetChild(1).GetComponent<Text>().text = "空";
            }
            else
            {
               Sort [i].transform.GetChild(0).GetComponent<Image>().sprite = Item [i + 1].img;
                Sort [i].transform.GetChild(1).GetComponent<Text>().text = Item [i + 1].name;
            }
        }

    }
    public void SavePlacedObjs()
    {
        PlayerPrefs.SetString("PlacedWeapon", Laidweapon.transform.GetChild(1).GetComponent<Text>().text);
        PlayerPrefs.SetString("PlacedArmor", Laidbody.transform.GetChild(1).GetComponent<Text>().text);
        PlayerPrefs.SetString("PlacedHelmet", Laidhead.transform.GetChild(1).GetComponent<Text>().text);
    }
    //public void ReadPlacedObjs()
    //{
    //    Laidweapon.transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString("PlacedWeapon");
    //    Laidbody.transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString("PlacedArmor");
    //    Laidhead.transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString("PlacedHelmet");
    //    switch (Laidweapon.transform.GetChild(1).GetComponent<Text>().text)
    //    {
    //        case "最好的剑":
    //            Laidweapon.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("01"); break;
    //        case "更好的剑":
    //            Laidweapon.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("02"); break;
    //    }
    //    switch (Laidbody.transform.GetChild(1).GetComponent<Text>().text)
    //    {
    //        case "最好的剑":
    //            Laidbody.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("01"); break;
    //        case "更好的剑":
    //            Laidbody.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("02"); break;
    //    }
    //    switch (Laidhead.transform.GetChild(1).GetComponent<Text>().text)
    //    {
    //        case "最好的剑":
    //            Laidhead.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("04"); break;
    //        case "更好的剑":
    //            Laidhead.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("05"); break;
    //    }
    //}
}



// Start is called before the first frame update
//void Awake()
//{
//    for (int i = 0; i < 4; i++)
//    {
//        weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
//        weapon[i].transform.GetChild(1).GetComponent<Text>().text = "空";
//    }
//    for (int i = 0; i < Object_WeaponBag.weaponsize; i++)
//    {
//        weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[i].img;
//        weapon[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[i].name;
//    }
//}

// Update is called once per frame

//switch(weaponchange)
//{
//    case 0:
//        //未装备武器时，确保武器按 添加进背包的时间先后顺序 排列
//        for (int i = 0; i < Object_WeaponBag.weaponsize; i++)
//        {
//            weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[i].img;
//            weapon[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[i].name;
//        }
//    break;

//    case 1://收到放入武器位的指令
//        ChangeWeapons(Laidweapon, weapon,Object_WeaponBag. weaponsize,changeWhich,Object_WeaponBag.Weapons);
//        if(Laidweapon.transform.GetChild(1).GetComponent<Text>().text == "最好的剑X1")
//        {
//            Player_Stats.ATK = 20;
//        }
//        weaponchange = 2;
//    break;

//    case 3://收到武器放回的指令

//        Laidweapon.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
//        Laidweapon.transform.GetChild(1).GetComponent<Text>().text = null;
//        for (int i = 0; i < Object_WeaponBag.weaponsize; i++)
//        {
//            weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[i].img;
//            weapon[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[i].name;
//        }
//        weaponchange = 0;
//    break;

//    case 4://收到交换武器指令
//        Sprite tempImage;
//        string tempText;
//        tempImage = Laidweapon.transform.GetChild(0).GetComponent<Image>().sprite;
//        tempText = Laidweapon.transform.GetChild(1).GetComponent<Text>().text;
//        Laidweapon.transform.GetChild(0).GetComponent<Image>().sprite = weapon[changeWhich].transform.GetChild(0).GetComponent<Image>().sprite;
//        Laidweapon.transform.GetChild(1).GetComponent<Text>().text = weapon[changeWhich].transform.GetChild(1).GetComponent<Text>().text;
//        weapon[changeWhich].transform.GetChild(0).GetComponent<Image>().sprite = tempImage;
//        weapon[changeWhich].transform.GetChild(1).GetComponent<Text>().text = tempText;
//        tempImage = null;
//        tempText = null;
//        weaponchange = 2;
//    break;
//}
