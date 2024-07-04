using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillTypeData", menuName = "SkillData/SkillTypeData")]
public class SkillDataSO : ScriptableObject
{
    public List<SkillTypeData> SkillTypeDatas = new List<SkillTypeData>();
}
[System.Serializable]
public class SkillTypeData
{
    public SkillType SkillType;
    public Sprite SkillIcon;
}
