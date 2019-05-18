using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RPGTALK.Helper;

[RequireComponent(typeof(Rigidbody2D))]
public class Events : MonoBehaviour
{
	public Camera cam;
	[Space]
	public GameManager gameManager;
	public GameObject player;
	public Player_Stats stats;
	public RPGTalk rpgTalkHolder;
	public RPGTalkCharacter temporaryNPC;
	public AudioClip sceneChangeSound;
	/// <summary>
	/// 交互物可旋转时用于交互物空闲时的图片，0下1左2上3右
	/// </summary>
	[Tooltip("交互物可旋转时用于交互物空闲时的图片，0下1左2上3右")]
	public Sprite[] idleSprites;
	[Space]
	public Sprite[] eventSprites;
	public AudioClip[] eventSounds;

	/// <summary>
	/// 角色朝向，仅供查看，任何情况下只能为Vector.down等四个方向的值
	/// </summary>
	[Tooltip("角色朝向，仅供查看，任何情况下只能为Vector.down等四个方向的值")]
	public Vector2 faceOrientation;
	/// <summary>
	/// 角色每次移动的目标
	/// </summary>
	protected Vector2 target;
	protected bool moving = false;
	bool inEvent = false;

	protected Rigidbody2D rb;
	protected AudioSource au;
	protected Animator an;

	void OnEnable()
	{
		rb = GetComponent<Rigidbody2D>();
		au = GetComponent<AudioSource>();
		an = GetComponent<Animator>();
		target = rb.position;
		
	}
	void Start()
	{
		switch (gameObject.name)
		{
			case "TestArea":
				if (Player_Stats.switchListInt[40] != 0) Destroy(gameObject);
				break;
			case "BestArmor":
				if (Player_Stats.switchListInt[2] != 0) gameObject.SetActive(false);
				break;
			case "BestSword":
				if (Player_Stats.switchListInt[3] != 0) gameObject.SetActive(false);
				break;
			case "BestHelmet":
				if (Player_Stats.switchListInt[4] != 0) gameObject.SetActive(false);
				break;
			case "Dragon":
				if (Player_Stats.switchListInt[20] != 0) Destroy(gameObject);
				break;
			case "TreasureBox":
				if (Player_Stats.switchListInt[5] != 0) Destroy(gameObject);
				break;
			case "Tutorial":
				if (Player_Stats.switchListInt[9] != 0) Destroy(gameObject);
				break;
		}
		if (rpgTalkHolder != null)
		{
			rpgTalkHolder.OnNewTalk += OnNewTalk;
			rpgTalkHolder.OnEndTalk += OnEndTalk;
		}
	}
    private void Update()
    {
        switch (gameObject.name)
        {
            case "AfterDragonBattle":
                if (Player_Stats.switchListInt[7] == 1) gameObject.SetActive(true);
                else gameObject.SetActive(false);
                break;
            
        }
    }
    /// <summary>
    /// 交互时被动方使用的方法
    /// </summary>
    /// <param name="calling">调用事件时的主动方</param>
    public void OnCall(GameObject calling)
	{
		inEvent = true;
		Events callingEvent = calling.GetComponent<Events>();
		switch (gameObject.name)
		{
			case "Passerby":
                    SetFaceOrientation(-callingEvent.faceOrientation);
                    CreateTemporaryNPC(temporaryNPC, "神秘的小姐姐", eventSprites[0]);
                    rpgTalkHolder.NewTalk("26", "38");
                break;
			case "TestBox":
				rpgTalkHolder.NewTalk("1", "1");
				break;
			case "TestArea":
				if (Player_Stats.switchListInt[40] == 0) 
				{
					if (GameManager.monstersJoining != null) GameManager.monstersJoining.Clear();
					if (GameManager.monstersJoining == null) GameManager.monstersJoining = new List<GameObject>();
					GameManager.monstersJoining.Add(GameManager.allMonsters[0]);
					GameManager.monstersJoining.Add(GameManager.allMonsters[0]);
					gameManager.StartBattle(GameManager.monstersJoining);
					Player_Stats.switchListInt[40] = 1;
					Destroy(gameObject);
				}
				break;
            case "BestArmor":
                rpgTalkHolder.NewTalk("5", "5");
                break;
            case "BestSword":
                rpgTalkHolder.NewTalk("4", "4");
                break;
            case "BestHelmet":
                rpgTalkHolder.NewTalk("6", "6");
                break;
            case "TreasureBox":
                rpgTalkHolder.NewTalk("7", "8");
                break;
            case "Dragon":
                rpgTalkHolder.NewTalk("21", "25");
                break;
            case "Notice":
                rpgTalkHolder.NewTalk("3", "3");
                break;
            case "Guard":
                if (Player_Stats.switchListInt[0] == 0)
                {
                    SetFaceOrientation(-callingEvent.faceOrientation);
					CreateTemporaryNPC(temporaryNPC, "守卫", eventSprites[0]);
					rpgTalkHolder.NewTalk("5", "9");
					Player_Stats.switchListInt[0] = 1;
				}
                break;
            case "King":
                if (Player_Stats.switchListInt[1] == 0)
                {
					CreateTemporaryNPC(temporaryNPC, "国王", eventSprites[0]);
					rpgTalkHolder.NewTalk("10", "20");
					Player_Stats.switchListInt[1] = 1;
				}
                break;
            case "Gate":
				au.PlayOneShot(sceneChangeSound);
				an.Play("GateOpen");
                gameManager.StartCoroutine("ChangeScene", "PalaceIn");
                Object_WeaponBag.save = true;
                break;
            case "StoreSign":
                gameManager.OpenStore();Input.ResetInputAxes();
                break;
            case "FogreSign":
                gameManager.OpenForge(); Input.ResetInputAxes();
                break;
            case "SynthesiSign":
                gameManager.OpenSynthesis(); Input.ResetInputAxes();
                break;
            case "Tutorial":
				rpgTalkHolder.NewTalk("9", "12");
				break;
            case "AfterDragonBattle":
                if (Player_Stats.switchListInt[7] == 1 && !GameManager.inBattle) 
                {
                    if (Player_Stats.HP > 0)
                    {
                        GameManager.fading = true;
                        GameObject.Find("Fading").GetComponent<Animator>().Play("FadeToBlack");
                        rpgTalkHolder.NewTalk("13", "16");
                    }
                }
                break;
            case "SceneMove01":
				au.PlayOneShot(sceneChangeSound);
				switch (SceneManager.GetActiveScene().name)
				{
					case "GrassLand":
						gameManager.StartCoroutine("ChangeScene", "Store");
						break;
					case "Store":
						gameManager.StartCoroutine("ChangeScene", "GrassLand");
						break;
                    case "Town":
                        gameManager.StartCoroutine("ChangeScene", "PalaceOut");
                        break;
                    case "PalaceOut":
                        gameManager.StartCoroutine("ChangeScene", "Town");
                        break;
                    case "SnowMountain":
                        gameManager.StartCoroutine("ChangeScene", "Store");
                        break;
					case "Cave":
                        gameManager.StartCoroutine("ChangeScene", "SnowMountain");
                        break;
                }
                Object_WeaponBag.save = true;
                break;
            case "SceneMove02":
                au.PlayOneShot(sceneChangeSound);
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Store":
                        gameManager.StartCoroutine("ChangeScene", "SnowMountain");
                        Object_WeaponBag.save = true;
                        break;
					case "SnowMountain":
						gameManager.StartCoroutine("ChangeScene", "Cave");
						break;
                }
                break;
        }
	}

    void OnDisable()
    {
		if (rpgTalkHolder != null)
		{
			rpgTalkHolder.OnNewTalk -= OnNewTalk;
			rpgTalkHolder.OnEndTalk -= OnEndTalk;
		}
	}
	/// <summary>
	/// 创建临时NPC(一般在该角色仅出现一次时使用)
	/// </summary>
	/// <param name="rpgTalkCharacter">RPG Talker组件</param>
	/// <param name="name">角色名</param>
	/// <param name="photo">角色图片</param>
	void CreateTemporaryNPC(RPGTalkCharacter rpgTalkCharacter, string name, Sprite photo)
	{
		rpgTalkCharacter.dialoger = name;
		rpgTalkCharacter.photo = photo;
	}
	/// <summary>
	/// 每次对话开始时自动调用
	/// </summary>
	void OnNewTalk()
	{
		Input.ResetInputAxes();
		GameManager.inScene = false;
	}
	/// <summary>
	/// 每次对话结束时自动调用
	/// </summary>
	void OnEndTalk()
	{
		if (inEvent)
		{
			switch (gameObject.name)
			{
				case "TestArea":
					if (Player_Stats.switchListInt[0] == 0)
					{
						Player_Stats.switchListInt[0] = 1;
						Destroy(gameObject);
					}
					break;
				case "BestArmor":
					Object_WeaponBag.armorsize += 1;
					Object_WeaponBag.Armors.Add(new Item("最好的甲", Resources.Load<Sprite>("06"), 0));
					Debug.Log("已经获取防具");
					if (Player_Stats.switchListInt[2] == 0)
					{
						Player_Stats.switchListInt[2] = 1;
						gameObject.SetActive(false);
					}
					break;
				case "BestSword":
					Object_WeaponBag.weaponsize += 1;
					Object_WeaponBag.Weapons.Add(new Item("最好的剑", Resources.Load<Sprite>("01"), 0));
					Debug.Log("已经获取武器");
					if (Player_Stats.switchListInt[3] == 0)
					{
						Player_Stats.switchListInt[3] = 1;
						gameObject.SetActive(false);
					}
					break;
                case "BestHelmet":
                    Object_WeaponBag.helmetsize += 1;
                    Object_WeaponBag.Helmets.Add(new Item("最好的头", Resources.Load<Sprite>("04"), 0));
                    Debug.Log("已经获取头盔");
                    if (Player_Stats.switchListInt[4] == 0)
                    {
						Player_Stats.switchListInt[4] = 1;
						gameObject.SetActive(false);
					}
                    break;
				case "Dragon":
					if (Player_Stats.switchListInt[20] == 0)
					{
						if (GameManager.monstersJoining != null) GameManager.monstersJoining.Clear();
						if (GameManager.monstersJoining == null) GameManager.monstersJoining = new List<GameObject>();
						GameManager.monstersJoining.Add(GameManager.allMonsters[1]);
						gameManager.StartBattle(GameManager.monstersJoining);
						Player_Stats.switchListInt[20] = 1;
                        Player_Stats.switchListInt[7] = 1;
                        Destroy(gameObject);
					}
					break;
				case "King":
                    GameObject.Find("Accessible").transform.Find("Weapons").gameObject.SetActive(true);
                    Invoke("LoadGrassLand", 15f);
                    break;
                case "TreasureBox":
                    if (Player_Stats.switchListInt[5] == 0)
                    {
                        Object_WeaponBag.weaponsize += 1;
                        Object_WeaponBag.Weapons.Add(new Item("更好的剑", Resources.Load<Sprite>("02"), 1));
                        Object_WeaponBag.armorsize += 1;
                        Object_WeaponBag.Armors.Add(new Item("更好的甲", Resources.Load<Sprite>("15"), 1));
                        Object_WeaponBag.helmetsize += 1;
                        Object_WeaponBag.Helmets.Add(new Item("更好的头", Resources.Load<Sprite>("05"), 1));
						Player_Stats.switchListInt[5] = 1;
						Destroy(gameObject);
					}
                    break;
				case "Tutorial":
                    if (Player_Stats.switchListInt[9] == 0)
                    {
						Player_Stats.switchListInt[9] = 1;
						Destroy(gameObject);
					}
                    break;
                case "AfterDragonBattle":
                    if (Player_Stats.switchListInt[7] == 1)
                    {
                        Player_Stats.switchListInt[7] = 2;
                        gameManager.StartCoroutine("ChangeScene", "Start");
                    }
                    break;
            }
			Input.ResetInputAxes();
			GameManager.inScene = true;
		}
		inEvent = false;
	}
	/// <summary>
	/// 设定角色朝向
	/// </summary>
	/// <param name="vector">角色朝向</param>
	public void SetFaceOrientation(Vector2 vector)
	{
		faceOrientation = vector;
		SetSprite();
	}
	/// <summary>
	/// 设定角色朝向与位置
	/// </summary>
	/// <param name="vector">角色朝向</param>
	/// <param name="pos">角色位置</param>
	public void SetFaceOrientation(Vector2 vector, Vector2 pos)
	{
		transform.position = pos;
		faceOrientation = vector;
		SetSprite();
	}
	/// <summary>
	/// 根据角色朝向改变角色图片
	/// </summary>
	public void SetSprite()
	{
		int x = 0;
		if (faceOrientation.x != 0) x = (int)faceOrientation.x + 1;
		GetComponent<SpriteRenderer>().sprite = idleSprites[x + (int)faceOrientation.y + 1];
	}
	/// <summary>
	/// 根据角色朝向改变角色行走动画
	/// </summary>
	public void SetWalkAnimation()
	{
		//这里用不了switch，理由为Vector.right等不是常量
		if (faceOrientation == Vector2.right) an.Play("Right");
		else if (faceOrientation == Vector2.left) an.Play("Left");
		else if (faceOrientation == Vector2.up) an.Play("Up");
		else an.Play("Down");
	}
	/// <summary>
	/// 检测角色面前是否存在障碍物
	/// </summary>
	/// <returns>角色面前是否存在障碍物</returns>
	protected virtual bool FaceObstacle()
	{
		LayerMask mask = LayerMask.GetMask("Obstacle");
		RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, faceOrientation, 1.4f, mask);
		LayerMask maskEdge = LayerMask.GetMask("Edge");
		RaycastHit2D hitEdge = Physics2D.Raycast(transform.position, faceOrientation, 1.1f, maskEdge);
		if (hit != null || hitEdge)
		{
			for (int i = 0; i < hit.Length; i++)
				if (hit[i] && !hit[i].collider.isTrigger)
				{
					if (hit[i].collider.tag == "Player") return false;
					else return true;
				}
			if (hitEdge && !hitEdge.collider.isTrigger) return true;
		}
		return false;
	}
	public void EndEvent()
	{
		Input.ResetInputAxes();
		GameManager.inScene = true;
	}
    public void LoadGrassLand()
    {
        gameManager.StartCoroutine("ChangeScene", "GrassLand");
        Object_WeaponBag.save = true;
        //ChangeWeapon.save = true;
    }
}
