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
[System.Serializable]
public class ObjectData
{
    [System.Serializable]
    public class ObjectInfo
    {
        public byte id;
        public string objectName;
        public string spritePath;

    }
    public ObjectInfo[] Objects;
}