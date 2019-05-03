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
                for (int i = whichObj; i < Object_WeaponBag.weaponsize-1; i++)
                {
                    Object_WeaponBag.Weapons[i].name = Object_WeaponBag.Weapons[i + 1].name;
                    Object_WeaponBag.Weapons[i].img= Object_WeaponBag.Weapons[i + 1].img;

                }
                Object_WeaponBag.Weapons[Object_WeaponBag.weaponsize-1].name="";
                Object_WeaponBag.Weapons[Object_WeaponBag.weaponsize-1].img= Resources.Load<Sprite>("00");
                Object_WeaponBag.weaponsize -= 1;
                break;
        }
    }
}
