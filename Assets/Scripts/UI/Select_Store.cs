using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Store : Select
{
    public GameObject Store;
    public GameObject Goods;
    public GameObject Sell;
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
            Store.SetActive(false);
        }
        if (Input.GetButtonDown("Submit"))
        {
            switch (states)
            {
                case 1: Goods.SetActive(true);gameObject.SetActive(false); break;
                case 2: Sell.SetActive(true); gameObject.SetActive(false); break;
                case 3: Store.SetActive(false);break;
            }
        }
    }
}
