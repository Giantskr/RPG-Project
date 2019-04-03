﻿using System.Collections;
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

    void Update()
    {
        weaponchange=Equip(weaponchange, weaponchangeWhich,Object_WeaponBag.weaponsize, weapon, Object_WeaponBag.Weapons, "最好的剑X1", 30,  Laidweapon);
        armorchange = Equip(armorchange, armorchangeWhich, Object_WeaponBag.armorsize, body, Object_WeaponBag.Armors, "", 0,  Laidbody);
    }
    protected int Equip(int change,int changeWhich,int size,List<GameObject> sorts ,List<Item> item,string name,int stastic,GameObject laid)
    {
        switch (change)
        {
            case 0:
                //未装备武器时，确保武器按 添加进背包的时间先后顺序 排列
                for (int i = 0; i < size; i++)
                {
                    sorts [i].transform.GetChild(0).GetComponent<Image>().sprite = item[i].img;
                    sorts[i].transform.GetChild(1).GetComponent<Text>().text = item[i].name;
                }
                break;

            case 1://收到放入武器位的指令
               ChangeWeapons(laid,sorts,size,changeWhich,item);
                if (laid.transform.GetChild(1).GetComponent<Text>().text == name)
                {
                    Player_Stats.ATK = stastic ;
                }
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
