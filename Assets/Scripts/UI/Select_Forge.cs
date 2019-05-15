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
    WeaponsData weapons;
    // Start is called before the first frame update
    void Start()
    {
        weapons = LoadJson<WeaponsData>.LoadJsonFromFile("Weapons");
        Describe.text = "锻造费用：$500"+"\n"+"按回车键锻造";
    }

    // Update is called once per frame
    void Update()
    {
        Selection();
        if (Input.GetButtonDown("Cancel"))
        {
            SelectForge.SetActive(true);
            objects.SetActive(false);
        }
        for (int i = 0; i < Object_WeaponBag.weaponsize + 1; i++)
        {
            //Debug.Log(i);
            if (i == Object_WeaponBag.weaponsize)
            {
                for (int j = 0; j < Object_WeaponBag.helmetsize; j++)
                {
                    if (j == Object_WeaponBag.helmetsize)
                    {
                        for (int k = 0; k < Object_WeaponBag.armorsize; k++)
                        {
                            Selections[j + i+k].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Armors[j].img;
                            Selections[j + i+k].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Armors[j].name;
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
        foreach (var data in weapons.Weapons)
            if (data.objectName == Object_WeaponBag.Weapons[states-1].name)
            {
                EquipmentStates.text = data.ATK + "\n" + data.DEF + "\n" + data.MAT + "\n" + data.MDF + "\n" + data.AGI;
            }

    }
}
