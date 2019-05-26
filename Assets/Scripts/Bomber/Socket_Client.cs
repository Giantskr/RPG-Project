using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Socket_Client : MonoBehaviour
{
	string ipAddress = "39.105.93.244";
	int port = 10001;
	public bool connected = false;                  //在线状态

	byte[] data = new byte[64];
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
	Bomber_Manager manager = null;
	Bomber_MatchManager match = null;

	string functionToCall = "";
	string message = "";

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
	}

	void Start()
	{
		SceneManager.sceneLoaded += ChangeScene;
		if (FindObjectOfType<Bomber_Manager>() != null)
			manager = FindObjectOfType<Bomber_Manager>();
		if (FindObjectOfType<Bomber_MatchManager>() != null)
			match = FindObjectOfType<Bomber_MatchManager>();
		connectCount = 0;
		if (!connected) ConnectToServer();
	}

	void Update()
	{
		if (functionToCall != "")
		{
			if (FindObjectOfType<Bomber_Manager>() != null)
				manager = FindObjectOfType<Bomber_Manager>();
			if (FindObjectOfType<Bomber_MatchManager>() != null)
				match = FindObjectOfType<Bomber_MatchManager>();
			switch (functionToCall)
			{
				case "Failed": match.MatchFail(); break;
				case "Success": if (match != null) match.StartCoroutine("MatchSuccess"); break;
				case "PlaceBomb": manager.EnemyAction(functionToCall); break;
				case "Moveright": manager.EnemyAction(functionToCall); break;
				case "Moveleft": manager.EnemyAction(functionToCall); break;
				case "Moveup": manager.EnemyAction(functionToCall); break;
				case "Movedown": manager.EnemyAction(functionToCall); break;
				case "Spawn": manager.ChangeItemState(true, message); break;
				case "Destroy": manager.ChangeItemState(false, message); break;
				case "Chat": manager.DisplayChat(false, message); break;
			}
			functionToCall = "";
			message = "";
		}
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
		//try
		//{
		//	connectCount++;
		//	Debug.Log("这是第" + connectCount + "次连接");
		//	clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		//	clientSocket.Connect(IPAddress.Parse(ipAddress), port);
		//	Debug.Log("连接服务器成功");
		//	StartCoroutine("ReceiveData");
		//	receiveT = new Thread(ReceiveData);
		//	receiveT.Start();
		//}
		//catch (Exception ex)
		//{
		//	Debug.Log("连接服务器失败！");
		//	Debug.Log(ex.Message);
		//	clientSocket = null;
		//	ConnectToServer();
		//}
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
			//StartCoroutine("ReceiveData");
		}
		else
		{
			Debug.LogWarning("连接失败");
			clientSocket = null;
			ConnectToServer();
		}
		//结束连接
		clientSocket.EndConnect(ar);
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
		ms = ms.PadRight(64, ' ');
		byte[] data = new byte[64];
		data = Encoding.UTF8.GetBytes(ms);
		clientSocket.Send(data);
		Debug.Log("Send: " + Encoding.UTF8.GetString(data, 0, data.Length));
	}

	void ReceiveData()
	{	
		while (true)
		{
			//yield return new WaitForEndOfFrame();
			try
			{
				int length = 0;
				length = clientSocket.Receive(data);
				if (connected && (!clientSocket.Connected || length == 0))
				{
					Debug.Log("与服务器断开连接");
					clientSocket = null;
					connected = false;
					ConnectToServer();
					break;
				}
				string str = Encoding.UTF8.GetString(data, 0, data.Length);
				Debug.Log("Receive: " + str);
				AnalizeData(str);
			}
			catch (Exception ex)
			{
				Debug.Log("连接服务器失败！");
				Debug.Log(ex.Message);
				clientSocket = null;
				ConnectToServer();
			}
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
						case "Match": type = ActionType.Match; break;
						case "Action": type = ActionType.Action; break;
						case "Item": type = ActionType.Item; break;
						case "Chat": type = ActionType.Chat; message = ms;functionToCall = i; break; 
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
						case "Failed": functionToCall = i; break;
						case "Success": functionToCall = i; break;
					}
					break;
				case ActionType.Action:
					switch (i)
					{
						case "PlaceBomb": functionToCall = i; break;
						case "Moveright": functionToCall = i; break;
						case "Moveleft": functionToCall = i; break;
						case "Moveup": functionToCall = i; break;
						case "Movedown": functionToCall = i; break;
					}
					break;
				case ActionType.Item:
					switch (i)
					{
						case "Spawn": message = ms;functionToCall = i; break;
						case "Destroy": message = ms; functionToCall = i; break;
					}
					break;
			}
		}
	}

	void ChangeScene(Scene scene, LoadSceneMode mode)
	{
		if (FindObjectOfType<Bomber_Manager>() != null)
			manager = FindObjectOfType<Bomber_Manager>();
		if (FindObjectOfType<Bomber_MatchManager>() != null)
			match = FindObjectOfType<Bomber_MatchManager>();
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
