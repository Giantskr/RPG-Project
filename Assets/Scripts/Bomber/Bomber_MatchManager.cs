using UnityEngine;
using UnityEngine.UI;
using System.Collections;


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
		socket.SendData("Match,Find,");

		//string hostname = Dns.GetHostName();
		//IPAddress[] ipadrlist = Dns.GetHostAddresses(hostname);
		//foreach (IPAddress ipadr in ipadrlist)
		//{
		//	if (ipadr.AddressFamily == AddressFamily.InterNetwork)
		//	{//判断是否IPv4
		//		socket.SendData(ipadr.ToString());
		//		break;
		//	}
		//}
	}
	public void CancelMatch()
	{
		findingText.text = "已取消匹配！";
		matching = false;
		socket.SendData("Match,Cancel,");
	}
	public void FailMatch()
	{
		findingText.text = "匹配失败，请重试！";
		matching = false;
	}
	public IEnumerator StartGame()
	{
		findingText.text = "你匹配到了" + Socket_Client.enemyName + "！";
		yield return new WaitForSeconds(1.5f);
		gameManager.StartCoroutine("ChangeScene", "Bomber_Game");
	}
}
