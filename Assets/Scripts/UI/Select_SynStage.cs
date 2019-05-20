using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_SynStage : Select
{

    public GameObject higerSelect;//用于切回上一级菜单
    public GameObject ThisSelect;//用于关闭本级菜单
    public GameObject LowerSelect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Selection();
        if (Input.GetButtonDown("Cancel"))
        {
            higerSelect.SetActive(true);
            ThisSelect.SetActive(false);
        }
        if (Input.GetButtonDown("Submit"))
        {
            
            switch (states)
            {
                case 1: case 2:
                    ThisSelect.SetActive(false);
                    LowerSelect.SetActive(true);
                    break;
                case 3:
                    if(Selections[0].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00")&& Selections[1].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00"))
                    {
                        Syn();
                    }
                    break;
                case 4:
                    if (Selections[0].transform.GetChild(0).GetComponent<Image>().sprite != Resources.Load<Sprite>("00"))
                    {
                        GetSyn();
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
