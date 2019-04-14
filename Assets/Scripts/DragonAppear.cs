using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DragonAppear : MonoBehaviour
{
    int time = 0;
    bool sceneLoaded = false;
    public GameManager gameManager;
    public GameObject dragon;
    public GameObject Player;
    public Sprite lay;
    public AudioClip Crowl;
    public AudioClip Coming;
    public AudioClip Attack;
    // Start is called before the first frame update
    void Start()
    {
        if (Player_Stats.switchListInt[6] != 0)
        {          
            Player.SetActive(true);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Submit"))
        {
            Player_Stats.switchListInt[6] = 1;
            Player_Stats.lastScene = "PalaceOut";
            gameManager.StartCoroutine("ChangeScene", "Town");
        }
        time += 1;
        if (time == 225)
        {
            Destroy(transform.GetChild(0).GetComponent<Animator>());
            Destroy(transform.GetChild(1).GetComponent<Animator>());
            Destroy(transform.GetChild(2).GetComponent<Animator>());
            dragon.SetActive(true);
            AudioSource.PlayClipAtPoint(Coming, new Vector3(0, 0, 0));
            AudioSource.PlayClipAtPoint(Crowl,new Vector3 (0,0,0));
        }
        if (time == 400)
        {
            AudioSource.PlayClipAtPoint(Attack , new Vector3(0, 0, 0));
            dragon.SetActive(false );
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = lay;
            transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = lay;
            Player_Stats.switchListInt[6] = 1;
        }
        if (time == 500)
        {
            Player_Stats.lastScene = "PalaceOut";
            gameManager.StartCoroutine("ChangeScene", "Town");
        }
    }
}
