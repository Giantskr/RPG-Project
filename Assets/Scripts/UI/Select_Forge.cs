using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Select_Forge :Select
{
    public GameObject SelectForge;
    public GameObject objects;
    public Text Describe;
    public Text EquipmentStates;
    public Text AfterForgeStates;
    WeaponsData weapons;
    HelmetsData helmets;
    ArmorsData armors;
    
    // Start is called before the first frame update
    void Start()
    {
        weapons = LoadJson<WeaponsData>.LoadJsonFromFile("Weapons");
        armors = LoadJson<ArmorsData>.LoadJsonFromFile("Armors");
        helmets = LoadJson<HelmetsData>.LoadJsonFromFile("Helmets");
        Describe.text = "锻造费用：$500"+"\n"+"按回车键锻造";
    }

    // Update is called once per frame
    void Update()
    {
        Selection();
        //显示是否已经锻造过
        foreach (var data in Object_WeaponBag.Weapons)
        {
           
            if (data.name == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text && data.num != 0)
            {
                Describe.text = "[已锻造]";
                foreach (var Data in weapons.Weapons)
                    if (Data.objectName == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text)
                    {
                        AfterForgeStates.text = Data.ATK + data.num + "\n" + Data.DEF + "\n" + Data.MAT + "\n" + Data.MDF + "\n" + Data.AGI;
                    }          
            }
            if (data.name == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text && data.num == 0)
            {
                Describe.text = "锻造费用：$500" + "\n" + "按回车键锻造";
                AfterForgeStates.text = "?" + "\n" + "?" + "\n" + "?" + "\n" + "?" + "\n" + "?";
            }
        }
        foreach (var data in Object_WeaponBag.Helmets)
        {
            Debug.Log("武器现在的数据是:" + data.num + data.name + Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text);
            if (data.name == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text && data.num != 0)
            {
                Describe.text = "[已锻造]";
                foreach (var Data in helmets.Helmets)
                    if (Data.objectName == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text)
                    {
                        AfterForgeStates.text = Data.ATK  + "\n" + Data.DEF + data.num + "\n" + Data.MAT + "\n" + Data.MDF + "\n" + Data.AGI;
                    }
            }
            if (data.name == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text && data.num == 0)
            {
                Describe.text = "锻造费用：$500" + "\n" + "按回车键锻造";
                AfterForgeStates.text = "?" + "\n" + "?" + "\n" + "?" + "\n" + "?" + "\n" + "?";
            }
        }
        foreach (var data in Object_WeaponBag.Armors)
        {
            if (data.name == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text && data.num != 0)
            {
                Describe.text = "[已锻造]";
                foreach (var Data in armors.Armors)
                    if (Data.objectName == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text)
                    {
                        AfterForgeStates.text = Data.ATK + "\n" + Data.DEF + "\n" + Data.MAT + "\n" + Data.MDF + data.num + "\n" + Data.AGI;
                    }
            }
            if (data.name == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text && data.num == 0)
            {
                Describe.text = "锻造费用：$500" + "\n" + "按回车键锻造";
                AfterForgeStates.text = "?" + "\n" + "?" + "\n" + "?" + "\n" + "?" + "\n" + "?";
            }
        }
        //取消键返回
        if (Input.GetButtonDown("Cancel"))
        {
            SelectForge.SetActive(true);
            objects.SetActive(false);
        }
        //submit功能 花掉钱然后锻造装备
        if (Input.GetButtonDown("Submit"))
        {
            foreach(var data in Object_WeaponBag.Weapons)
            {
                int i = 0;
                if (data.name== Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text&&data.num==0)
                {
                    Player_Stats.money -= 500;
                    data.num += 5;
                    PlayerPrefs.SetInt("Weapon" + i, data.num); 
                }
                i++;
            }
            foreach (var data in Object_WeaponBag.Helmets)
            {
                int j=0;
                if (data.name == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text && data.num == 0)
                {
                    Player_Stats.money -= 500;
                    data.num += 5;
                    PlayerPrefs.SetInt("Helmet" + j, data.num);
                }
                j++;
            }
            foreach (var data in Object_WeaponBag.Armors)
            {
                int k=0;
                if (data.name == Selections[states - 1].transform.GetChild(1).GetComponent<Text>().text && data.num == 0)
                {
                    Player_Stats.money -= 500;
                    data.num += 5;
                    PlayerPrefs.SetInt("Armor" + k, data.num);
                }
                k++;
            }

        }
        //以下为在UI中显示所有 武器 头盔 盔甲 的代码
        for (int i = 0; i < Object_WeaponBag.weaponsize + 1; i++)
        {
            //Debug.Log(i);
            if (i == Object_WeaponBag.weaponsize)
            {
                for (int j = 0; j < Object_WeaponBag.helmetsize+1; j++)
                {
                    if (j == Object_WeaponBag.helmetsize)
                    {
                        for (int k = 0; k < Object_WeaponBag.armorsize; k++)
                        {
                            Selections[j + i+k].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Armors[k].img;
                            Selections[j + i+k].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Armors[k].name;
                        }
                    }
                    else
                    {
                        Selections[j + i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Helmets[j].img;
                        Selections[j + i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Helmets[j].name;
                    }
                }
            }
            else
            {
                Selections[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[i].img;
                Selections[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[i].name;
            }
        }
        //以下为显示选中装备属性的代码
        foreach (var data in weapons.Weapons)
            if (data.objectName == Selections[states-1].transform.GetChild(1).GetComponent<Text>().text)
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
