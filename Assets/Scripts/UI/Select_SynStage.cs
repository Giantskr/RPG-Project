using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_SynStage : Select
{

    public GameObject higerSelect;//用于切回上一级菜单
    public GameObject ThisSelect;//用于关闭本级菜单
    public GameObject LowerSelect;

    public Text Describe;

    private bool isSyn = false;
    // Start is called before the first frame update
    void Start()
    {
        Describe.text = "选择想要合成的两件物品(同种类)，花费：500G";
        states = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Selections[0].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00")&& Selections[1].transform.GetChild(0).GetComponent<Image>().sprite == Resources.Load<Sprite>("00"))
        {
            states = 2;
        }
        if (Selections[0].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00") && Selections[1].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00"))
        {
            states = 3;
        }
        if (isSyn)
        {
            states = 4;
        }
        transform.position = Selections[states-1].transform.position;
        if (Input.GetButtonDown("Cancel"))//按退出键后 一切复位
        {
            Select_Syn.returnn = true;
            Selections[0].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
            Selections[1].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
            states = 1;
            higerSelect.SetActive(true);
            ThisSelect.SetActive(false);
        }
        if (Input.GetButtonDown("Submit"))
        {
            
            switch (states)
            {
                case 1: case 2:
                    Select_Syn.OneOrTwo = states;
                    ThisSelect.SetActive(false);
                    LowerSelect.SetActive(true);
                    break;
                case 3:
                    if(Selections[0].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00")&& Selections[1].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00"))
                    {
                        Syn();
                        Selections[0].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
                        Selections[1].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
                    }
                    break;
                case 4:
                    if (Selections[0].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00"))
                    {
                        GetSyn();
                        Selections[3].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("00");
                        isSyn = false;states = 1;
                    }
                    break;
            }
        }
    }
    public void Syn()
    {

    }
    public void GetSyn()
    {

    }
}
