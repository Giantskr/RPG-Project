using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object_WeaponBag : MonoBehaviour
{
    //public GameObject WeaponLaid;
    public List<GameObject> weapon,prop;
    public static List<Item> Weapons,Props;
    public static  int weaponsize,propsize = 0;
    //public static Sprite[] sprites;
    // Start is called before the first frame update
    private void Awake()
    {
        Weapons = new List<Item>(); // 初始化List<Item>
        Props = new List<Item>();

        for (int i = 0; i < weaponsize; i++)
        {
            weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Weapons[i].img;
            weapon[i].transform.GetChild(1).GetComponent<Text>().text = Weapons[i].name;
        }
        for (int i = 0; i < propsize; i++)
        {
            prop[i].transform.GetChild(0).GetComponent<Image>().sprite = Props[i].img;
            prop[i].transform.GetChild(1).GetComponent<Text>().text = Props[i].name;
        }
    }

    //public static Transform FindChild(Transform trans, string goName)
    //{
    //    Transform child = trans.Find(goName);
    //    if (child != null)
    //        return child;
    //        return null;
    //}
    // Update is called once per frame
    void Update()
    {
        //sprites = new Sprite[size];
        for (int i = 0; i < weaponsize; i++)
        {
            weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Weapons[i].img;
            weapon[i].transform.GetChild(1).GetComponent<Text>().text = Weapons[i].name;
            //.Log("233");
            //FindChild(weapon[0].transform, "icon").GetComponent<image>()
        }
        for (int i = 0; i < propsize; i++)
        {
            prop[i].transform.GetChild(0).GetComponent<Image>().sprite = Props[i].img;
            prop[i].transform.GetChild(1).GetComponent<Text>().text = Props[i].name;
        }
    }
}
