﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Syn : Select
{
    public GameObject SelectSyn;
    public GameObject objects;
    public GameObject[] syn;
    public GameObject AfterSyn;

    public Text Describe;
    public Text EquipmentStates;

    WeaponsData weapons;
    HelmetsData helmets;
    ArmorsData armors;
 
    public static int OneOrTwo;//当前在选择第一个物品还是第二个物品

    public static bool returnn=false;
    
    void Start()
    {
        ShowObj();
        weapons = LoadJson<WeaponsData>.LoadJsonFromFile("Weapons");
        armors = LoadJson<ArmorsData>.LoadJsonFromFile("Armors");
        helmets = LoadJson<HelmetsData>.LoadJsonFromFile("Helmets");
    }

    // Update is called once per frame
    void Update()
    {
        Selection();
        ShowObjState();
        if (returnn == true)
        {
            ShowObj();
            for (int i = 0; i < 6; i++)
            {
                Selections[i].transform.GetChild(2).gameObject.SetActive(false);
            }
            returnn = false;
        }
        if (Input.GetButtonDown("Cancel"))
        {
            SelectSyn.SetActive(true);
            objects.SetActive(false);
        }
        if (Input.GetButtonDown("Submit"))
        {
            if(Selections[states - 1].transform.GetChild(0).GetComponent<Image>().sprite== Resources.Load<Sprite>("00") || (OneOrTwo==2&& syn[0].GetComponent<Image>().sprite == Selections[states - 1].transform.GetChild(0).GetComponent<Image>().sprite))
            {
                GameManager.whichSound=2;
                Describe.text = "请选择同种类的不同装备。";
            }
            if((OneOrTwo ==1&& Selections[states - 1].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00")) || (OneOrTwo == 2 && syn[0].GetComponent<Image>().sprite != Selections[states - 1].transform.GetChild(0).GetComponent<Image>().sprite&& Selections[states - 1].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00")))
            {
                ChangeObj(syn[OneOrTwo - 1], Selections);
                SelectSyn.SetActive(true);
                objects.SetActive(false);
            }          
        }
    }
    public void ChangeObj(GameObject Laid, List<GameObject> Sort)
    {
        Laid.GetComponent<Image>().sprite = Sort[states-1].transform.GetChild(0).GetComponent<Image>().sprite;
        Sort[states - 1].transform.GetChild(2).gameObject.SetActive(true);
    }
    public void ShowObj()
    {
        for (int i = 0; i < 4; i++)
        {
            Selections[i].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
            Selections[i].transform.GetChild(1).GetComponent<Text>().text = "空";
        }
        for (int i = 0; i < Object_WeaponBag.weaponsize + 1; i++)
        {
            //Debug.Log(i);
            if (i == Object_WeaponBag.weaponsize)
            {
                for (int j = 0; j < Object_WeaponBag.helmetsize + 1; j++)
                {
                    if (j == Object_WeaponBag.helmetsize)
                    {
                        for (int k = 0; k < Object_WeaponBag.armorsize; k++)
                        {
                            Selections[j + i + k].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Armors[k].img;
                            Selections[j + i + k].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Armors[k].name;
                            Selections[j + i + k].transform.GetChild(2).transform.position = Selections[j + i + k].transform.position;
                        }
                    }
                    else
                    {
                        Selections[j + i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Helmets[j].img;
                        Selections[j + i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Helmets[j].name;
                        Selections[j + i].transform.GetChild(2).transform.position = Selections[j + i].transform.position;
                    }
                }
            }
            else
            {
                Selections[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[i].img;
                Selections[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[i].name;
                Selections[i].transform.GetChild(2).transform.position = Selections[i].transform.position;
            }
        }
    }
    public void ShowObjState()
    {
        foreach (var data in weapons.Weapons)
            if (data.objectName == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text)
            {
                EquipmentStates.text = data.ATK + "\n" + data.DEF + "\n" + data.MAT + "\n" + data.MDF + "\n" + data.AGI;
            }
        foreach (var data in helmets.Helmets)
            if (data.objectName == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text)
            {
                EquipmentStates.text = data.ATK + "\n" + data.DEF + "\n" + data.MAT + "\n" + data.MDF + "\n" + data.AGI;
            }
        foreach (var data in armors.Armors)
            if (data.objectName == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text)
            {
                EquipmentStates.text = data.ATK + "\n" + data.DEF + "\n" + data.MAT + "\n" + data.MDF + "\n" + data.AGI;
            }
    }
}