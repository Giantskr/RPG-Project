using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Equip : Select
{
    public GameObject UI_Weapons;
    public GameObject Weapons;//父物体
    public GameObject[] weapon;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        Selection();
        ChangeWeapon.changeWhich = states - 1;
        if (Input.GetButtonDown("Cancel"))
        {
            UI_Weapons.SetActive(true);
            Weapons.SetActive(false);
        }
        if (Input.GetButtonDown("Submit"))
        {
            switch (states)
            {
                case 1:if(weapon[0].transform.GetChild(1).GetComponent<Text>().text == "空")
                    {
                        Debug.Log("233");
                        ChangeWeapon.change = 3;
                    }
                    else
                    {
                        Debug.Log("996");
                        ChangeWeapon.change = 1;
                    }
                    UI_Weapons.SetActive(true);
                    Weapons.SetActive(false);
                break;
                case 2:
                    if(weapon[1].transform.GetChild(1).GetComponent<Text>().text =="空"  ) 
                    {
                        Debug.Log("233");
                        ChangeWeapon.change = 3;
                    }
                    else
                    {
                        Debug.Log("996");
                        ChangeWeapon.change = 1;
                    }
                    
                    UI_Weapons.SetActive(true);
                    Weapons.SetActive(false);
                    break;
                case 3:
                    ChangeWeapon.change = 1;
                    UI_Weapons.SetActive(true);
                    Weapons.SetActive(false);
                    break;
                case 4:
                    ChangeWeapon.change = 1;
                    UI_Weapons.SetActive(true);
                    Weapons.SetActive(false);
                    break;
            }
        }
       
    }
}
