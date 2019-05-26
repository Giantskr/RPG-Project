using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Bomber_MatchManager : MonoBehaviour
{
	Socket_Client socket;
	public GameManager gameManager;
	public GameObject loginUI;
	public GameObject matchUI;
	public InputField username;
	public InputField password;
	public Text welcomeText;
	public Text findingText;
	public Text loginType;
	public Text failText;
	public Text[] buttonText; 
	public static short matching = 0;
	bool login = true;
	static bool match = false;

	void OnEnable()
    {
		matching = 0;
		socket = FindObjectOfType<Socket_Client>();
		if (!socket.connected) socket.ConnectToServer();
		matchUI.SetActive(match);
		loginUI.SetActive(!match);
		username.text = "";
		password.text = "";
		failText.text = "";
		findingText.text = "";
		welcomeText.text = "欢迎 " + Socket_Client.userName + " 来到炸弹人游戏！";
	}

	public void LoginAction()
	{
		bool fail = true;
		if (username.text.Length < 3 || username.text.Length > 10) failText.text = "用户名应为3-10个英文或数字！";
		else if (password.text.Length < 6 || password.text.Length > 14) failText.text = "密码应为6-14个字符！";
		else fail = false;
		if (!fail)
		{
			failText.text = "";
			if (login) socket.Login(username.text, password.text);
			else socket.Register(username.text, password.text);
		}
	}

	public void ChangeLoginType()
	{
		username.text = "";
		password.text = "";
		failText.text = "";
		if (login)
		{
			loginType.text = "注册";
			buttonText[0].text = "注册";
			buttonText[1].text = "返回登录";
			login = false;
		}
		else
		{
			loginType.text = "登录";
			buttonText[0].text = "登录";
			buttonText[1].text = "转到注册";
			login = true;
		}
	}

	public void ChangeMode()
	{
		bool mode = loginUI.activeInHierarchy;
		loginUI.SetActive(!mode);
		matchUI.SetActive(mode);
		login = true;
		username.text = "";
		password.text = "";
		failText.text = "";
		findingText.text = "";
		welcomeText.text = "欢迎 " + Socket_Client.userName + " 来到炸弹人游戏！";
	}

	public void FindMatch()
	{
		matching = 1;
		findingText.text = "正在寻找游戏......";
		socket.SendData("Match,Find,");
	}

	public void CancelMatch()
	{
		matching = 0;
		findingText.text = "已取消匹配！";
		socket.SendData("Match,Cancel,");
	}
	public void MatchFail()
	{
		matching = 0;
		findingText.text = "匹配失败，请重试！";
	}
	public IEnumerator MatchSuccess()
	{
		matching = 2;
		findingText.text = "成功匹配！";
		yield return new WaitForSeconds(2);
		gameManager.StartCoroutine("ChangeScene", "Bomber_Game");
	}
}
