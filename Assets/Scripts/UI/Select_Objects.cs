using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Objects : Select
{
    public GameObject UI_Selections;
    public GameObject UI_Objects;
    public List<GameObject> Sorts;

    void Start()
    {
        
    }

    void Update()
    {
        Selection();
        if (Input.GetButtonDown("Cancel"))
        {
            UI_Selections.SetActive(true);
            UI_Objects.SetActive(false);
        }
		else if (true)
		{
            
            Sorts[states-1].SetActive(true);
            switch (states)
            {
                case 1:Sorts[1].SetActive(false); Sorts[2].SetActive(false); Sorts[3].SetActive(false);break;
                case 2: Sorts[0].SetActive(false); Sorts[2].SetActive(false); Sorts[3].SetActive(false); break;
                case 3: Sorts[0].SetActive(false); Sorts[1].SetActive(false); Sorts[3].SetActive(false); break;
                case 4: Sorts[0].SetActive(false); Sorts[1].SetActive(false); Sorts[2].SetActive(false); break;
            }
        }
	}
}
