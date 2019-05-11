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
        public string introduction;
        public int price;
        public string hpFormula;
        public string mpFormula;
    }
    public ObjectInfo[] Objects;
}
[System.Serializable]
public class WeaponsData
{
    [System.Serializable]
    public class WeaponsInfo
    {
        public byte id;
        public string objectName;
        public string spritePath;
        public string introduction;
        public int level;
        public int price;
        public int ATK;
        public int DEF;
        public int MAT;
        public int MDF;
        public int AGI;
        public int maxHP;
        public int maxMP;
        public int plusDmg;
    }
    public WeaponsInfo[] Weapons;
}
[System.Serializable]
public class ArmorsData
{
    [System.Serializable]
    public class ArmorsInfo
    {
        public byte id;
        public string objectName;
        public string spritePath;
        public string introduction;
        public int price;
        public int ATK;
        public int DEF;
        public int MAT;
        public int MDF;
        public int AGI;
        public int maxHP;
        public int maxMP;
        public int resistance;

    }
    public ArmorsInfo[] Armors;
}
public class HelmetsData
{
    [System.Serializable]
    public class HelmetsInfo
    {
        public byte id;
        public string objectName;
        public string spritePath;
        public string introduction;
        public int price;
        public int ATK;
        public int DEF;
        public int MAT;
        public int MDF;
        public int AGI;
        public int maxHP;
        public int maxMP;
        public int resistance;

    }
    public HelmetsInfo[] Helmets;
}