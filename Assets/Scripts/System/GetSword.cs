using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSword : ItemModel
{
   
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Submit"))
        {
            Object_WeaponBag.size += 1;
            if (Object_WeaponBag.Weapons != null)
            {
                Debug.Log(Object_WeaponBag.Weapons.Count);
            }
            Object_WeaponBag.Weapons.Add(new ItemModel.Item("初等剑", Resources.Load<Sprite>("01")));
            Debug.Log("emmm");
        }
    }
}
