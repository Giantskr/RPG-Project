using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object_WeaponBag : ItemModel
{
    //public GameObject WeaponLaid;
    public List<GameObject> weapon;
    public static List<Item> Weapons;
    public static  int size = 0;
    //public static Sprite[] sprites;
    // Start is called before the first frame update
    private void Awake()
    {
        Weapons = new List<Item>(); // 初始化List<Item>
        //sprites = new Sprite[size];
        for (int i = 0; i < size; i++)
        {
            weapon[i].transform.GetChild(0).GetComponent<Image>().sprite = Weapons[i].img;
            weapon[i].transform.GetChild(1).GetComponent<Text>().text = Weapons[i].name;
            Debug.Log("233");
            //FindChild(weapon[0].transform, "icon").GetComponent<image>()
        }
    }
    void  update() // 数据初始化
    {
       
        
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
        
    }
}
