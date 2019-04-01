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
    public static int change = 0;//0 for not equip;1 for equipping;2 for had equipped;3 for reset;
    public static int changeWhich;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
            weapon[i].transform.GetChild(1).GetComponent<Text>().text = "空";
        }
        for (int i = 0; i < Object_WeaponBag.weaponsize; i++)
        {
            weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[i].img;
            weapon[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[i].name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (change == 0)
        {
            for (int i = 0; i < Object_WeaponBag.weaponsize; i++)
            {
                weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[i].img;
                weapon[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[i].name;
            }
        }
        if (change == 1)
        {
            ChangeWeapons(changeWhich);
            change = 2;
        }
        if (change == 3)
        {
            Laidweapon.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
            Laidweapon.transform.GetChild(1).GetComponent<Text>().text = null;
            for (int i = 0; i < Object_WeaponBag.weaponsize; i++)
            {
                weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[i].img;
                weapon[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[i].name;
            }
            change = 0;
        }
    }
    protected void ChangeWeapons(int changeWhich)
    {
        Laidweapon.transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[changeWhich].img;
        Laidweapon.transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[changeWhich].name;
        for (int i = changeWhich; i < Object_WeaponBag.weaponsize; i++)
        {
            
            if (i ==1)
            {
                weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
                weapon[i].transform.GetChild(1).GetComponent<Text>().text = "空";
            }
            else
            {
                weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[i + 1].img;
                weapon[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[i + 1].name;
            }
        }

        //    if (weapon[changeWhich + 1].transform.GetChild(1).GetComponent<Text>().text != "空")
        //    {
        //        remove(changeWhich);
        //        Debug.Log(Object_WeaponBag.Weapons[0].name);
        //        if (weapon[changeWhich + 2].transform.GetChild(1).GetComponent<Text>().text != "空")
        //        {
        //            remove(changeWhich + 1);
        //            if(weapon[changeWhich + 3].transform.GetChild(1).GetComponent<Text>().text != "空")
        //            {
        //                remove(changeWhich + 2);
        //            }
        //            else
        //            {
        //                reset(changeWhich + 2);
        //            }
        //        }
        //        else
        //        {
        //            reset(changeWhich + 1);
        //        }
        //    }
        //    else
        //    {
        //        reset(changeWhich);
        //    }

        //}
        //void remove(int Which)
        //{
        //    weapon[Which].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[Which+1].img;
        //    weapon[Which].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[Which + 1].name;  
        //}
        //void reset(int Which)
        //{
        //    weapon[changeWhich].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
        //    weapon[changeWhich].transform.GetChild(1).GetComponent<Text>().text = "空";
        //}
    }
}
