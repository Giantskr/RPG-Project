using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stats : MonoBehaviour
{
    public GameObject UI_Selections;
    public GameObject UI_State;
    [Tooltip("0 for level," +
        "1 for HP" +
        "2 for MP" +
        "3 for EXP" +
        "4 for next level" +
        "5 for Attack" +
        "6 for Defense" +
        "7 for MagicAttack" +
        "8 for MagicDefense" +
        "9 for Agility" +
        "10 for Lucky")]
    public GameObject[] elements;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            UI_Selections.SetActive(true);
            UI_State.SetActive(false);
        }
        elements[0].GetComponent<Text>().text = "" + Player_Stats.level;
        elements[1].GetComponent<Text>().text = Player_Stats.HP + "/" + Player_Stats.maxHP;
        elements[2].GetComponent<Text>().text = Player_Stats.MP + "/" + Player_Stats.maxMP;
        elements[3].GetComponent<Text>().text = "" + Player_Stats.EXP;
        elements[4].GetComponent<Text>().text = "" + Player_Stats.EXPToNextLevel;
        elements[5].GetComponent<Text>().text = "" + Player_Stats.ATK;
        elements[6].GetComponent<Text>().text = "" + Player_Stats.DEF;
        elements[7].GetComponent<Text>().text = "" + Player_Stats.MAT;
        elements[8].GetComponent<Text>().text = "" + Player_Stats.MDF;
        elements[9].GetComponent<Text>().text = "" + Player_Stats.AGI;
        elements[10].GetComponent<Text>().text = "" + Player_Stats.LUC;
    }
}
