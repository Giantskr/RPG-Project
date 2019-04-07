using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Select_Skill : Select
{
    public GameObject UI_Selections;
    public GameObject UI_Skills;
    public Text Describe;

    void Start()
    {
        //StreamReader streamreader = new StreamReader(Application.dataPath + "/StreamingAssets/Test.json");//读取数据，转换成数据流
        //JsonReader js = new JsonReader(streamreader);//再转换成json数据
        //Root r = JsonMapper.ToObject<Root>(js);//读取
        //for (int i = 0; i < r.TestData.Count; i++)//遍历获取数据

    }

    void Update()
    {
        for (int i = 0; i < Object_WeaponBag.skillsize; i++)
        {
            Selections[i].transform.GetChild(0).GetComponent<Image>().sprite = Object_WeaponBag.Skills[i].img;
            Selections[i].transform.GetChild(1).GetComponent<Text>().text = Object_WeaponBag.Skills[i].name;
        }
            Selection();
        
        switch (states)
        {
            case 1: Describe.text =" 对单体敌人造成大量伤害。"; break;
            case 2:if (Object_WeaponBag.skillsize >= 2) { Describe.text = " 使用者恢复一定量的HP。"; } else { Describe.text = null; }; break;
        }
       
        if (Input.GetButtonDown("Cancel"))
        {
            UI_Selections.SetActive(true);
            UI_Skills.SetActive(false);
        }
		
	}
}
