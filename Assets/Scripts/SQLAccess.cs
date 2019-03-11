using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SqlAccess
{
	//网上找的封装类，现抄，略改，方便用类似输入sql语句的方式调用

	public static MySqlConnection mySqlConnection;
	//数据库名称
	public static string database = "Cloud_Save";
	//数据库IP
	private static string host = "106.13.88.104";
	//用户名
	private static string username = "client";
	//用户密码
	private static string password = "client";

	public static string sql = string.Format("database={0};server={1};user={2};password={3};port={4}",
	database, host, username, password, "3306");

	public static MySqlConnection con;
	private MySqlCommand com;

	#region BaseOperation
	/// <summary>
	/// 构造方法开启数据库
	/// </summary>
	public SqlAccess()
	{
		con = new MySqlConnection(sql);
		OpenMySQL(con);
	}
	/// <summary>
	/// 启动数据库
	/// </summary>    
	/// <param name="con"></param>
	public void OpenMySQL(MySqlConnection con)
	{
		con.Open();
		Debug.Log("数据库连接中");
	}
	/// <summary>
	/// 插入数据
	/// </summary>
	/// <param name="sql"></param>
	/// <param name="con"></param>
	public void InsertInfo(string sql, MySqlConnection con)
	{
		MySqlCommand com = new MySqlCommand(sql, con);
		int res = com.ExecuteNonQuery();
	}
	/// <summary>
	/// 修改数据
	/// </summary>
	/// <param name="sql"></param>
	/// <param name="con"></param>
	public void UpdateInfo(string sql, MySqlConnection con)
	{
		MySqlCommand com = new MySqlCommand(sql, con);
		int res = com.ExecuteNonQuery();
	}
	/// <summary>
	/// 查询数据
	/// </summary>
	/// <param name="sql"></param>
	/// <param name="con"></param>
	public Dictionary<int, List<string>> QueryInfo(string sql, MySqlConnection con)
	{
		int indexDic = 0;
		int indexList = 0;
		Dictionary<int, List<string>> dic = new Dictionary<int, List<string>>();
		MySqlCommand com = new MySqlCommand(sql, con);
		MySqlDataReader reader = com.ExecuteReader();
		while (true)
		{
			if (reader.Read())
			{
				List<string> list = new List<string>();
				for (int i = 0; i < reader.FieldCount; i++)
				{
					list.Add(reader[indexList].ToString());
					indexList++;
				}
				dic.Add(indexDic, list);
				indexDic++;
				indexList = 0;
			}
			else
			{
				break;
			}
		}
		return dic;
	}
	/// <summary>
	/// 关闭数据库
	/// </summary>
	public void CloseMySQL()
	{
		(new MySqlConnection(sql)).Close();
		Debug.Log("关闭数据库");
	}
	#endregion
}
