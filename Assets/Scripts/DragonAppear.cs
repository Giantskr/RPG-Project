﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAppear : MonoBehaviour
{
    int time = 0;
    public GameObject dragon;
    public Sprite lay;
    public AudioClip crowl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += 1;
        if (time == 225)
        {
            Destroy(transform.GetChild(0).GetComponent<Animator>());
            Destroy(transform.GetChild(1).GetComponent<Animator>());
            Destroy(transform.GetChild(2).GetComponent<Animator>());
            dragon.SetActive(true);
            AudioSource.PlayClipAtPoint(crowl,new Vector3 (0,0,0));
        }
        if (time > 400)
        {
            dragon.SetActive(false );
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = lay;
            transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = lay;
        }
    }
}
