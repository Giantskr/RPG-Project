using System;
using System.Collections.Generic;
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
		None, Match, Action, Item, Chat
	}

	SqlAccess sqlAce;   //引用封装类
	MySqlConnection con;
	public static string userName, enemyName;

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
		Bomber_MatchManager match = FindObjectOfType<Bomber_MatchManager>();
		pw = Encrypt(pw);
		bool fail = false;
		try
		{
			sqlAce = new SqlAccess();
			con = SqlAccess.con;
			string sql = ("select username from user ");
			Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql, con);             //字典在封装类中
			for (int i = 0; i < dic.Count; i++)             //用户名查重
			{
				if (dic[i][0] == name)
				{
					match.failText.text = "用户名已经存在，请更换新的用户名！";
					fail = true;
					break;
				}
			}
			sqlAce.CloseMySQL();
			if (!fail)                                      //查重通过后添加用户
			{
				sqlAce = new SqlAccess();
				con = SqlAccess.con;
				sql = string.Format("insert into user(username,password) values('{0}','{1}')", name, Encrypt(pw));
				sqlAce.InsertInfo(sql, con);
				match.failText.text = "注册成功！请返回登录界面。";
				match.username.text = "";
				match.password.text = "";
			}
		}
		catch (Exception e)
		{
			match.failText.text = "未知错误，可能是因为未连接到服务器。";
			Debug.Log(e.ToString()); return;
		}
		finally { sqlAce.CloseMySQL(); }
	}

	public void Login(string name, string pw)
	{
		Bomber_MatchManager match = FindObjectOfType<Bomber_MatchManager>();
		pw = Encrypt(pw);
		bool fail = true;
		try
		{
			sqlAce = new SqlAccess();
			con = SqlAccess.con;
			string sql = ("select username,password from user ");
			Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql, con);
			for (int i = 0; i < dic.Count; i++)
			{
				if (dic[i][0] == name && dic[i][1] == Encrypt(pw))
				{
					fail = false;
					userName = name;
					match.ChangeMode();
					break;
				}
			}
			if (fail) match.failText.text = "用户名或密码错误！";
		}
		catch (Exception e)
		{
			match.failText.text = "未知错误，可能是因为未连接到服务器。";
			Debug.Log(e.ToString()); return;
		}
		finally { sqlAce.CloseMySQL(); }
	}

	string Encrypt(string pw)
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
		Bomber_Manager manager = FindObjectOfType<Bomber_Manager>();
		Bomber_MatchManager match = FindObjectOfType<Bomber_MatchManager>();
		foreach (string i in sArray)
		{
			switch (type)
			{
				case ActionType.None:
					switch (i)
					{
						case "Match": type = ActionType.Match; break;
						case "Action": type = ActionType.Action; break;
						case "Item": type = ActionType.Item; break;
						case "Chat": type = ActionType.Chat; manager.DisplayChat(false, ms); break; 
					}
					break;
				case ActionType.Match:
					if (i.StartsWith("Name")) enemyName = i.Remove(0, 4);
					else
					switch (i)
					{
						case "Player1":
								Bomber_Manager.playerPos = new Vector2(-7.5f, 4.5f);
								Bomber_Manager.enemyPos = new Vector2(8.5f, -5.5f);
								break;
						case "Player2":
								Bomber_Manager.playerPos = new Vector2(8.5f, -5.5f);
								Bomber_Manager.enemyPos = new Vector2(-7.5f, 4.5f);
								break;
						case "Failed": match.FailMatch(); break;
						case "Success": match.StartGame(); break;
					}
					break;
				case ActionType.Action: manager.EnemyAction(i); break;
				case ActionType.Item:
					switch (i)
					{
						case "Spawn": manager.ChangeItemState(true, ms); break;
						case "Destroy": manager.ChangeItemState(false, ms); break;
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
		catch (Exception ex) { Debug.Log(ex.Message); }
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
		catch (Exception ex) { Debug.Log(ex.Message); }
	}
}
