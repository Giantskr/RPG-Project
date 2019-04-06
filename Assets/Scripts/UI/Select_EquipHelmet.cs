using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_EquipHelmet : Select
{
    public GameObject UI_Weapons;
    public GameObject Helmet;//父物体
    public GameObject[] helmet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Selection();
        ChangeWeapon.helmetchangeWhich = states - 1;
        if (Input.GetButtonDown("Cancel"))
        {
            UI_Weapons.SetActive(true);
            Helmet.SetActive(false);
        }
        if (Input.GetButtonDown("Submit"))
        {
            ButtonEvent(states - 1);

        }

    }
    public void ButtonEvent(int i)
    {
        if (helmet [i].transform.GetChild(1).GetComponent<Text>().text == "空")
        {
            //Debug.Log("233");
            ChangeWeapon.helmetchange = 3;
        }
        else
        {
            if (ChangeWeapon.helmetchange == 2)
            {
                ChangeWeapon.helmetchange = 4;
            }
            else
            {
                //Debug.Log("996");
                ChangeWeapon.helmetchange = 1;
            }

        }
        UI_Weapons.SetActive(true);
        Helmet.SetActive(false);
    }
}
