using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/// MVC模式——Model层，定义物品结构，保存物品数据

public class ItemModel : MonoBehaviour
{

    // 物品类的定义
    public class Item
    {
        public string name; // 物品名称
        public Sprite img;  // 物品图片

        // 构造器
        public Item(string name, Sprite img)
        {
            this.name = name;
            this.img = img;
        }
    }

}