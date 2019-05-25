using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSword : MonoBehaviour
{
    public bool isGet=false;
    private int num=0;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("u") )
        {
            num += 1;
            if (isGet == false)
            {
                Object_WeaponBag.weaponsize += 1;
                Object_WeaponBag.Weapons.Add(new Item("最好的剑", Resources.Load<Sprite>("01"), 0));
                Object_WeaponBag.weaponsize += 1;
                Object_WeaponBag.Weapons.Add(new Item("更好的剑", Resources.Load<Sprite>("02"), 0));
            }

            //if (Object_WeaponBag.Weapons != null)
            //{
            //    Debug.Log(Object_WeaponBag.Weapons.Count);
            //}
            //if (isGet == false)
//
            //Object_WeaponBag.Weapons.Add(Item.Item1);
            // Debug.Log("emmm");

        }
    }
}
