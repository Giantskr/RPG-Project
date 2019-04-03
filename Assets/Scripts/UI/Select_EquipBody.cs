using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_EquipBody : Select 
{
    public GameObject UI_Weapons;
    public GameObject Bodyshield;//父物体
    public GameObject[] bodyshield;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Selection();
        ChangeWeapon.armorchangeWhich = states - 1;
        if (Input.GetButtonDown("Cancel"))
        {
            UI_Weapons.SetActive(true);
            Bodyshield.SetActive(false);
        }
        if (Input.GetButtonDown("Submit"))
        {
            ButtonEvent(states - 1);

        }

    }
    public void ButtonEvent(int i)
    {
        if (bodyshield[i].transform.GetChild(1).GetComponent<Text>().text == "空")
        {
            //Debug.Log("233");
            ChangeWeapon.armorchange = 3;
        }
        else
        {
            if (ChangeWeapon.armorchange == 2)
            {
                ChangeWeapon.armorchange = 4;
            }
            else
            {
                //Debug.Log("996");
                ChangeWeapon.armorchange = 1;
            }

        }
        UI_Weapons.SetActive(true);
        Bodyshield.SetActive(false);
    }
}
