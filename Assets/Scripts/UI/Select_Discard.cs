using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Discard : Select
{
    public GameObject higerSelect;//用于切回上一级菜单
    public GameObject ThisSelect;//用于关闭本级菜单
    //public GameObject[] objects;//用于选择要丢弃的物品
    public string sort;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Selection();
        //OptionNum = Selections.Length;
        if (Input.GetButtonDown("Cancel"))
        {
            ThisSelect.gameObject.SetActive(false);
            higerSelect.SetActive(true);
        }
        if (Input.GetButtonDown("Submit"))
        {
            Discard(sort, states - 1);
        }
    }
    public void Discard(string sort,int whichObj)
    {
        switch (sort)
        {
            case "Weapon":
                for (int i = 0; i < Object_WeaponBag.weaponsize; i++)
                {
                    //if(i==Object_WeaponBag.weaponsize)
                    //{
                    //    Object_WeaponBag.Weapons[i] = Object_WeaponBag.Weapons[i + 1];
                    //}
                    //else
                   
                        Object_WeaponBag.Weapons[i] = null;
                        Object_WeaponBag.weaponsize -= 1;
                                 
                }

                break;
        }
    }
}
