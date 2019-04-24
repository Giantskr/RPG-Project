using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SkillModel : MonoBehaviour
{

	
}

[System.Serializable]
public class SkillData
{
	[System.Serializable]
	public class SkillInfo
	{
		public byte id;
		public string skillName;
		public string spritePath;
		public string introduce;
		public int mpCost;
		public float accuracy;
		public string formula;
	}
	public SkillInfo[] Skills;
}
public class ObjectData
{

    public class ObjectInfo
    {
        public byte id;
        public string ObjectName;
        public string spritePath;

    }
    public ObjectInfo[] Objects;
}