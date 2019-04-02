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
    public static int change = 0;
    //0 for not equip;
    //1 for equipping;
    //2 for had equipped;
    //3 for reset;
    //4 for Change;
    //requests are received from Select_Equip.cs
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
        switch(change)
        {
            case 0:
                //未装备武器时，确保武器按 添加进背包的时间先后顺序 排列
                for (int i = 0; i < Object_WeaponBag.weaponsize; i++)
                {
                    weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[i].img;
                    weapon[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[i].name;
                }
            break;

            case 1://收到放入武器位的指令
                ChangeWeapons(changeWhich);
                if(Laidweapon.transform.GetChild(1).GetComponent<Text>().text == "最好的剑X1")
                {
                    Player_Stats.ATK = 20;
                }
                change = 2;
            break;

            case 3://收到武器放回的指令

                Laidweapon.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
                Laidweapon.transform.GetChild(1).GetComponent<Text>().text = null;
                for (int i = 0; i < Object_WeaponBag.weaponsize; i++)
                {
                    weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[i].img;
                    weapon[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[i].name;
                }
                change = 0;
            break;

            case 4://收到交换武器指令
                Sprite tempImage;
                string tempText;
                tempImage = Laidweapon.transform.GetChild(0).GetComponent<Image>().sprite;
                tempText = Laidweapon.transform.GetChild(1).GetComponent<Text>().text;
                Laidweapon.transform.GetChild(0).GetComponent<Image>().sprite = weapon[changeWhich].transform.GetChild(0).GetComponent<Image>().sprite;
                Laidweapon.transform.GetChild(1).GetComponent<Text>().text = weapon[changeWhich].transform.GetChild(1).GetComponent<Text>().text;
                weapon[changeWhich].transform.GetChild(0).GetComponent<Image>().sprite = tempImage;
                weapon[changeWhich].transform.GetChild(1).GetComponent<Text>().text = tempText;
                tempImage = null;
                tempText = null;
                change = 2;
            break;
        }
       
    }
    protected void ChangeWeapons(int changeWhich)
    {
        //图片，名字传入装备栏
        Laidweapon.transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Weapons[changeWhich].img;
        Laidweapon.transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Weapons[changeWhich].name;
        //整理将武器装备后 背包内剩余武器的排列
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
        //   之前的过于冗长的 还有bug 的代码
        //
        //
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
