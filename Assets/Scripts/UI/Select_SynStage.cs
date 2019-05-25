using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_SynStage : Select
{

    public GameObject higerSelect;//用于切回上一级菜单
    public GameObject ThisSelect;//用于关闭本级菜单
    public GameObject LowerSelect;

    public Text Describe;

    private bool isSyn = false;
    // Start is called before the first frame update
    void Start()
    {
        Describe.text = "选择想要合成的两件物品(同种类)，花费：500G";
        states = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Selections[0].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00")&& Selections[1].transform.GetChild(0).GetComponent<Image>().sprite == Resources.Load<Sprite>("00"))
        {
            states = 2;
        }
        if (Selections[0].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00") && Selections[1].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00"))
        {
            states = 3;
        }
        if (isSyn)
        {
            states = 4;
        }
        transform.position = Selections[states-1].transform.position;
        if (Input.GetButtonDown("Cancel")&&states!=3)//按退出键后 一切复位
        {
            Select_Syn.returnn = true;
            Selections[0].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
            Selections[1].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
            states = 1;
            higerSelect.SetActive(true);
            ThisSelect.SetActive(false);
        }
        if (Input.GetButtonDown("Submit"))
        {
            
            switch (states)
            {
                case 1: case 2:
                    Select_Syn.OneOrTwo = states;
                    ThisSelect.SetActive(false);
                    LowerSelect.SetActive(true);
                    break;
                case 3:
                    if(Selections[0].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00")&& Selections[1].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00"))
                    {
                        Syn();                      
                    }
                    break;
                case 4:
                    if (Selections[3].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00"))
                    {
                        GetSyn();
                        Selections[3].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
                        isSyn = false;
                        Describe.text = "选择想要合成的两件物品(同种类)，花费：500G";
                        states = 1;
                    }
                    break;
            }
        }
    }
    public void Syn()
    {
        bool SameSort=false;
        foreach (var data in Object_WeaponBag.Weapons)
        {
           
            int i = 0;
            if (data.img == Selections[0].transform.GetChild(0).GetComponent<Image>().sprite && data.num == 0)
            {

                Player_Stats.money -= 500;
                data.num += 5;
                SameSort = true;
                PlayerPrefs.SetInt("Weapon" + i, data.num);
            }
            i++;
        }
        foreach (var data in Object_WeaponBag.Weapons)
        {
            int i = 0;
            if (data.img == Selections[1].transform.GetChild(0).GetComponent<Image>().sprite && data.num == 0)
            {
                for (int j = i; j < Object_WeaponBag.weaponsize - 1; j++)
                {
                    Debug.Log(j);
                    Object_WeaponBag.Weapons[j].name = Object_WeaponBag.Weapons[j + 1].name;
                    Object_WeaponBag.Weapons[j].img = Object_WeaponBag.Weapons[j + 1].img;

                }
                Object_WeaponBag.Weapons[Object_WeaponBag.weaponsize - 1].name = "";
                Object_WeaponBag.Weapons[Object_WeaponBag.weaponsize - 1].img = Resources.Load<Sprite>("00");
                Object_WeaponBag.weaponsize -= 1;
                Object_WeaponBag.save = true;
            }
            else
            {
                i++;
            }
        }
        if (SameSort)
        {
            Selections[3].transform.GetChild(0).GetComponent<Image>().sprite = Selections[0].transform.GetChild(0).GetComponent<Image>().sprite;           
            SameSort = false;
            isSyn = true;
        }
        else
        {
            GameManager.whichSound = 2;
            Describe.text = "请选择同种类的不同装备。";
            Select_Syn.returnn = true;
            states = 1;
        }
        Selections[0].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
        Selections[1].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
    }
    public void GetSyn()
    {
        Describe.text = "请取走锻造好的装备。";
        Selections[3].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
    }
}
