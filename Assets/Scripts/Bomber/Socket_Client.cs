using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using MySql.Data.MySqlClient;
using UnityEngine;

public class Socket_Client : MonoBehaviour
{
	string ipAddress = "39.105.93.244";
	int port = 10001;
	public bool connected = false;                  //在线状态

	byte[] data = new byte[1024];
	Socket clientSocket;                            //客户端Socket
	int connectCount;                           //连接次数
	Thread receiveT;
	public static Socket_Client instance = null;
	enum ActionType
	{
		None, Match, Action
	}

	SqlAccess sqlAce;   //引用封装类
	MySqlConnection con;

	public static string enemyAddress;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
	} 

	void Start()
	{
		
		connectCount = 0;
		ConnectToServer();
	}

	//连接到服务器
	public void ConnectToServer()
	{
		connectCount++;
		Debug.Log("这是第" + connectCount + "次连接");
		//创建新的连接
		clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		//设置端口号和ip地址
		EndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
		//发起连接
		clientSocket.BeginConnect(endPoint, OnConnectCallBack, "");
	}

	//连接的回调
	public void OnConnectCallBack(IAsyncResult ar)
	{
		if (clientSocket.Connected)
		{
			Debug.Log("连接成功");
			connected = true;
			connectCount = 0;
			receiveT = new Thread(ReceiveData);
			receiveT.Start();
		}
		else
		{
			Debug.LogWarning("连接失败");
			clientSocket = null;
			ConnectToServer();
		}
		//结束连接
		//clientSocket.EndConnect(ar);
	}

	void Update()
	{
		CheckConnection();
	}

	public void Register(string name, string pw)
	{
		pw = Encypt(pw);
	}

	public void Login(string name, string pw)
	{
		pw = Encypt(pw);
	}

	string Encypt(string pw)
	{
		var bytes = Encoding.Default.GetBytes(pw);
		var SHA = new SHA1CryptoServiceProvider();
		var encryptbytes = SHA.ComputeHash(bytes);
		return Convert.ToBase64String(encryptbytes);
	}

	public void SendData(string ms)
	{
		byte[] data = new byte[1024];
		data = Encoding.UTF8.GetBytes(ms);
		clientSocket.Send(data);
		Debug.Log("Send: " + Encoding.UTF8.GetString(data, 0, data.Length));
	}

	public void ReceiveData()
	{
		while (true)
		{
			if (clientSocket.Connected == false)
			{
				Debug.Log("与服务器断开了连接");
				break;
			}
			int length = 0;
			length = clientSocket.Receive(data);
			string str = Encoding.UTF8.GetString(data, 0, data.Length);
			Debug.Log("Receive: "+str);
			AnalizeData(str);
		}
	}
	void AnalizeData(string ms)
	{
		ActionType type = ActionType.None;
		string[] sArray = Regex.Split(ms, ",", RegexOptions.IgnoreCase);
		foreach (string i in sArray)
		{
			switch (type)
			{
				case ActionType.None:
					switch (i)
					{
						case "Match":
							type = ActionType.Match;
							break;
						case "Action":
							type = ActionType.Action;
							break;
					}
					break;
				case ActionType.Match:
					Bomber_MatchManager match = FindObjectOfType<Bomber_MatchManager>();
					if (i.StartsWith("IP"))
					{
						enemyAddress = i.Remove(1);
					}
					else
					switch (i)
					{
						case "Failed":
							match.FailMatch();
							break;
						case "Success":
							match.StartGame();
							break;
					}
					break;
			}
		}
	}

	void CheckConnection()
	{
		if (connected && !clientSocket.Connected)
		{
			Debug.Log("与服务器断开连接");
			clientSocket = null;
			connected = false;

			ConnectToServer();
		}
	}
	void OnDestroy()
	{
		try
		{
			if (clientSocket != null)
			{
				clientSocket.Shutdown(SocketShutdown.Both);
				clientSocket.Close();//关闭连接
			}

			if (receiveT != null)
			{
				receiveT.Interrupt();
				receiveT.Abort();
			}

		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}
	void OnApplicationQuit()
	{
		try
		{
			if (clientSocket != null)
			{
				clientSocket.Shutdown(SocketShutdown.Both);
				clientSocket.Close();//关闭连接
			}

			if (receiveT != null)
			{
				receiveT.Interrupt();
				receiveT.Abort();
			}

		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}
}
