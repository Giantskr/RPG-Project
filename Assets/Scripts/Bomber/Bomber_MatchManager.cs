using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;

public class Bomber_MatchManager : MonoBehaviour
{
	Socket_Client socket;
	public GameManager gameManager;
	public Text findingText;
	public static bool matching;
	void OnEnable()
    {
		matching = false;
		socket = FindObjectOfType<Socket_Client>();
		findingText.text = " ";
	}

    void Update()
    {
        
    }

	public void FindMatch()
	{
		matching = true;
		findingText.text = "正在寻找游戏......";
		//		string hostName = Dns.GetHostName();   //获取本机名
		//#pragma warning disable CS0618 // 类型或成员已过时
		//		IPHostEntry localhost = Dns.GetHostByName(hostName);    //方法已过期，可以获取IPv4的地址
		//#pragma warning restore CS0618 // 类型或成员已过时
		//		//IPAddress[] localhost = Dns.GetHostAddresses(Dns.GetHostName());
		//		//IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址
		//		IPAddress localaddr = localhost.AddressList[0];

		string hostname = Dns.GetHostName();
		IPAddress[] ipadrlist = Dns.GetHostAddresses(hostname);
		foreach (IPAddress ipadr in ipadrlist)
		{
			if (ipadr.AddressFamily == AddressFamily.InterNetwork)
			{//判断是否IPv4
				socket.SendData("Match,IP"+ipadr.ToString());
				break;
			}
		}
		//socket.SendData(localaddr.ToString());
	}
	public void FailMatch()
	{
		findingText.text = "匹配失败，请重试";
		matching = false;
	}
	public void StartGame()
	{
		findingText.text = "你匹配到了" + Socket_Client.enemyAddress + "！";
		//gameManager.StartCoroutine("ChangeScene", "Bomber_Game");
	}
}
