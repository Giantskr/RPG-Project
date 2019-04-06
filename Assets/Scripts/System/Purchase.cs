using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
    public static List<Item> Goods;
    public List<GameObject> good;
    public static int goodsize;
    // Start is called before the first frame update
    private void Awake()
    {
        Goods = new List<Item>();
        goodsize += 6;
        Goods.Add(new Item("小瓶生命药剂" , Resources.Load<Sprite>("07"), 1));
        Goods.Add(new Item("中瓶生命药剂", Resources.Load<Sprite>("08"), 1));
        Goods.Add(new Item("大瓶生命药剂", Resources.Load<Sprite>("09"), 1));
        Goods.Add(new Item("小瓶魔力药剂", Resources.Load<Sprite>("12"), 1));
        Goods.Add(new Item("中瓶魔力药剂", Resources.Load<Sprite>("13"), 1));
        Goods.Add(new Item("大瓶魔力药剂", Resources.Load<Sprite>("14"), 1));
        for (int i = 0; i < goodsize; i++)
        {
            good[i].transform.GetChild(0).GetComponent<Image>().sprite = Goods[i].img;
            good[i].transform.GetChild(1).GetComponent<Text>().text = Goods[i].name;
        }
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
