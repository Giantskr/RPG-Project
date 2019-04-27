using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object_WeaponBag : MonoBehaviour
{
       //用于在物品栏显示各种物品，不限于武器
    //public GameObject WeaponLaid;
    public List<GameObject> weapon,prop,armor;
    public static List<Item> Weapons,Props,Helmets,Armors,Skills;
    public static int weaponsize,propsize,helmetsize,armorsize,skillsize = 0;
    public static bool save = false;
    public static Object_WeaponBag instance = null;

    ObjectData objects;

    private void Awake()
    {
        objects = LoadJson<ObjectData>.LoadJsonFromFile("Objects");
        //DontDestroyOnLoad(gameObject);
        //if (instance == null)
        //    instance = this;
        //else if (instance != this)
        //    Destroy(gameObject);

        if (Weapons == null)
        {
            Weapons = new List<Item>(); // 初始化List<Item>
            Props = new List<Item>();
            Helmets = new List<Item>();
            Armors = new List<Item>();
            Skills = new List<Item>();
            skillsize += 1;
            Skills.Add(new Item("强击", Resources.Load<Sprite>("10"), 1));
            
        }
        else
        {
            readObjectInformation();
        }
        
        
        //for (int i = 0; i < weaponsize; i++)
        //{
        //    weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Weapons[i].img;
        //    weapon[i].transform.GetChild(1).GetComponent<Text>().text = Weapons[i].name;
        //}
        //for (int i = 0; i < propsize; i++)
        //{
        //    prop[i].transform.GetChild(0).GetComponent<Image>().sprite = Props[i].img;
        //    prop[i].transform.GetChild(1).GetComponent<Text>().text = Props[i].name;
        //}
        //for (int i = 0; i <armorsize; i++)
        //{
        //    armor[i].transform.GetChild(0).GetComponent<Image>().sprite = Armors[i].img;
        //    armor[i].transform.GetChild(1).GetComponent<Text>().text = Armors[i].name;
        //}
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
        
        // Debug.Log(weaponsize);
        if (save)
        {
            saveObjectInformation();
            save = false;
        }
        //sprites = new Sprite[size];
        if (weaponsize == 0)
        {
            weapon[0].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
            weapon[0].transform.GetChild(1).GetComponent<Text>().text = "空";
        }
        for (int i = 0; i < weaponsize; i++)
        {
            Debug.Log(weaponsize);
                weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Weapons[i].img;
                weapon[i].transform.GetChild(1).GetComponent<Text>().text = Weapons[i].name;
            Debug.Log("233");
            //FindChild(weapon[0].transform, "icon").GetComponent<image>()
        }
        for (int i = 0; i < propsize; i++)
        {
            prop[i].transform.GetChild(0).GetComponent<Image>().sprite = Props[i].img;
            //if (Props[i].num != 0)
            //{
                prop[i].transform.GetChild(1).GetComponent<Text>().text = Props[i].name + "X" + Props[i].num;
            //}
            //else
            //{
            //    prop[i].transform.GetChild(1).GetComponent<Text>().text = null;
            //}
        }
        for (int i = 0; i <armorsize+1; i++)
        {
            //Debug.Log(i);
            if (i == armorsize)
            { 
                for (int j =0; j < helmetsize; j++)
                {
                    armor[j+i].transform.GetChild(0).GetComponent<Image>().sprite = Helmets[j].img;
                    armor[j+i].transform.GetChild(1).GetComponent<Text>().text = Helmets[j].name;
                }
            }
            else
            {
                armor[i].transform.GetChild(0).GetComponent<Image>().sprite = Armors[i].img;
                armor[i].transform.GetChild(1).GetComponent<Text>().text = Armors[i].name;
            }
        }
    }
    public void saveObjectInformation()
    {
        for (int i = 0; i < weaponsize; i++)
        {
            PlayerPrefs.SetString("Weapon" + i.ToString(), Weapons[i].name); PlayerPrefs.SetInt("weaponsize", weaponsize);
        }
        for (int i = 0; i < propsize; i++)
        {
            PlayerPrefs.SetString("Prop" + i.ToString(), Props[i].name); PlayerPrefs.SetInt("PropNum" + i, Props[i].num); PlayerPrefs.SetInt("propsize", propsize);
        }
        for (int i = 0; i < helmetsize; i++)
        {
            PlayerPrefs.SetString("Helmet" + i.ToString(), Helmets[i].name); PlayerPrefs.SetInt("helmetsize", helmetsize);
        }
        for (int i = 0; i < armorsize; i++)
        {
            PlayerPrefs.SetString("Armor" + i.ToString(), Armors[i].name); PlayerPrefs.SetInt("armorsize",armorsize);
        }
    }
    public void readObjectInformation()
    {
        //weaponsize = PlayerPrefs.GetInt("weaponsize");
        //propsize = PlayerPrefs.GetInt("propsize");
        //helmetsize = PlayerPrefs.GetInt("helmetsize");
        //armorsize = PlayerPrefs.GetInt("armorsize");
        for (int i = 0; i < weaponsize; i++)
        {
            Weapons[i].name = PlayerPrefs.GetString("Weapon" + i.ToString());witchWeapon(i,Weapons[i].name);
        }
        for (int i = 0; i < armorsize; i++)
        {
            Armors[i].name = PlayerPrefs.GetString("Armor" + i.ToString()); witchArmor(i, Armors[i].name);
        }
        for (int i = 0; i < weaponsize; i++)
        {
            Helmets[i].name = PlayerPrefs.GetString("Helmet" + i.ToString()); witchHelmet(i, Helmets[i].name);
        }
        for (int i = 0; i < propsize; i++)
        {
            Props[i].name = PlayerPrefs.GetString("Prop" + i.ToString());Props[i].num = PlayerPrefs.GetInt("PropNum" + i);
        }
    }
    public void witchWeapon(int i,string WeaponName)
    {
        Weapons[i].num = 1;
        foreach (var data in objects.Objects)
            if (data.objectName == WeaponName)
            {
                Weapons[i].img = Resources.Load<Sprite>(data.spritePath);
            }
        //        switch (WeaponName)
        //{
        //    case "最好的剑": Weapons[i].img = Resources.Load<Sprite>("01");break;           
            
        //    case "更好的剑": Weapons[i].img = Resources.Load<Sprite>("02"); break;            
        //   //修改这里
        //}
    }
    public void witchArmor(int i,string ArmorName)
    {
        Armors[i].num = 1;
        foreach (var data in objects.Objects)
            if (data.objectName == ArmorName)
            {
                Armors[i].img = Resources.Load<Sprite>(data.spritePath);
            }
    }
    public void witchHelmet(int i, string HelmetName)
    {
        Helmets[i].num = 1;
        foreach (var data in objects.Objects)
            if (data.objectName == HelmetName)
            {
                Helmets[i].img = Resources.Load<Sprite>(data.spritePath);
            }
    }
    public void witchProp(int i,string PropName)
    {
        foreach (var data in objects.Objects)
            if (data.objectName == PropName)
            {
                Props[i].img = Resources.Load<Sprite>(data.spritePath);
            }
    }
}
