using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// MVC模式——Model层，定义物品结构，保存物品数据
class ItemModel : MonoBehaviour
{

}
// 物品类的定义
public class Item
{
    public string name; // 物品名称
    public Sprite img;  // 物品图片
    public int num; //对于药品 num表示数量，对于武器 num表示属性
    // 构造器
    public Item(string name, Sprite img, int num)
    {
        this.name = name;
        this.img = img;
        this.num = num;
    }
}