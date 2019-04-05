using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadJson<T> : MonoBehaviour
{
	/// <summary>
	/// 从Json文件中读取数据
	/// </summary>
	/// <param name="FileName">文件名（文件必须是.json文件）</param>
	/// <returns></returns>
	public static T LoadJsonFromFile(string FileName)
	{
		if (!File.Exists(Application.dataPath + "/Resources/Models/" + FileName + ".json"))
		{
			return default;
		}
		StreamReader sr = new StreamReader(Application.dataPath + "/Resources/Models/" + FileName + ".json");
		if (sr == null)
		{
			return default;
		}
		string json = sr.ReadToEnd();
		if (json.Length > 0)
		{
			return JsonUtility.FromJson<T>(json);
		}
		return default;
	}
}