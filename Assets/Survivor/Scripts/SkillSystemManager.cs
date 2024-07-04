using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using SCN.Common;

public class SkillSystemManager : SerializedMonoBehaviour
{

    [SerializeField]
    Dictionary<SkillType, BaseActiveSkill> ActiveSkillPrefabs;

    [SerializeField]
    Dictionary<SkillType, bool> FollowPlayerDict;

    [SerializeField]
    Transform PlayerTransform;

    [SerializeField] List<SkillType> UnownedActiveSkill;

    [SerializeField] List<PassiveType> UnownedPassiveSkill;
    
    int maxActiveSkillCount;
    
    int maxPassiveSkillCount;

    List<PassiveType> MaximumPassiveList;

    List<SkillType> MaximumSkillList;

    [SerializeField]
    Dictionary<SkillType, int> RandomSkillTypeDict;

    [SerializeField]
    Dictionary<PassiveType, int> RandomPassiveTypeDict;

    [SerializeField]
    Dictionary<SkillType, int> ChosenSkillTypeDict;

    [SerializeField]
    Dictionary<PassiveType, int> ChosenPassiveTypeDict;

    [SerializeField] Dictionary<PassiveType, List<SkillType>> EvoPassiveDict;

    Dictionary<SkillType, PassiveType> EvoActiveDict;

    Dictionary<SkillType, bool> CanEvoDict;

    [SerializeField] Button btn;

    int skillCount;

    int passiveCount;

    public static SkillSystemManager Ins;
    private void Awake()
    {
        if (!Ins)
        {
            Ins = this;
        }
        RandomSkillTypeDict = new Dictionary<SkillType, int>();
        RandomPassiveTypeDict = new Dictionary<PassiveType, int>();
        ChosenSkillTypeDict = new Dictionary<SkillType, int>();
        ChosenPassiveTypeDict = new Dictionary<PassiveType, int>();
        MaximumSkillList = new List<SkillType>();
        MaximumPassiveList = new List<PassiveType>();
    }

    private void Start()
    {
        btn.onClick.AddListener(ShowSkillChoicePopup);
    }


    private void OnDestroy()
    {
    }
    
    private void ShowSkillChoicePopup()
    {
        skillCount = UnityEngine.Random.Range(1, 2);
        passiveCount = 3 - skillCount;

        RandomizeSkills(skillCount);
        RandomizePassives(passiveCount);

        SkillChoicePopup.Ins.OnShowSkillChoice(RandomSkillTypeDict, RandomPassiveTypeDict, false);
    }

    private void RandomizeSkills(int count)
    {
        RandomNoRepeat<SkillType> randomIndex;
        SkillType selectedSkill;
        for (int i = 0; i < count; i++)
        {
            if (UnownedActiveSkill.Count == 0) break;
            if (HasReachedMaxActiveSkills())
            {
                randomIndex = new RandomNoRepeat<SkillType>(ChosenSkillTypeDict.Keys);
            }
            else
            {
                randomIndex = new RandomNoRepeat<SkillType>(UnownedActiveSkill);
            }
            selectedSkill = randomIndex.Random();
            UnityEngine.Debug.Log("Skill: " + selectedSkill);
            if (!HasReachedMaxActiveSkills())
                MaximumSkillList.Add(selectedSkill);

            AddValueActiveSkill(selectedSkill);
        }
    }

    private void RandomizePassives(int count)
    {
        RandomNoRepeat<PassiveType> randomIndex;
        PassiveType selectedPassive;
        for (int i = 0; i < count; i++)
        {
            if (UnownedActiveSkill.Count == 0) break;
            if (HasReachedMaxActiveSkills())
            {
                randomIndex = new RandomNoRepeat<PassiveType>(ChosenPassiveTypeDict.Keys);
            }
            else
            {
                randomIndex = new RandomNoRepeat<PassiveType>(UnownedPassiveSkill);
            }
            selectedPassive = randomIndex.Random();
            UnityEngine.Debug.Log("Passive: " + selectedPassive);
            if (!HasReachedMaxActiveSkills())
                MaximumPassiveList.Add(selectedPassive);
            
            AddValuePassiveSkill(selectedPassive);
        }
    }

    public bool HasReachedMaxPassiveSkills()
    {
        if (maxPassiveSkillCount >= 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasReachedMaxActiveSkills()
    {
        if (maxActiveSkillCount >= 5)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void AddValueActiveSkill(SkillType type)
    {
        UnityEngine.Debug.Log(ChosenSkillTypeDict.ContainsKey(type));
        if (ChosenSkillTypeDict.ContainsKey(type))
        {
            RandomSkillTypeDict[type] = ChosenSkillTypeDict[type];
            RandomSkillTypeDict[type] += 1;
        }
        else
        {
            RandomSkillTypeDict.Add(type, 1);
        }
    }

    public void AddValuePassiveSkill(PassiveType type)
    {
        UnityEngine.Debug.Log(ChosenPassiveTypeDict.ContainsKey(type));
        if (ChosenPassiveTypeDict.ContainsKey(type))
        {
            RandomPassiveTypeDict[type] = ChosenPassiveTypeDict[type];
            RandomPassiveTypeDict[type] += 1;
        }
        else
        {
            RandomPassiveTypeDict.Add(type, 1);
        }
    }

    public void OnSelectSkillType(SkillType skillType, byte skillGradeIndex)
    {
        if (!ChosenSkillTypeDict.ContainsKey(skillType))
        {
            ChosenSkillTypeDict.Add(skillType, skillGradeIndex);
            maxActiveSkillCount += 1;
        }
        else
        {
            ChosenSkillTypeDict[skillType] += 1;
            if (HasActiveSkillMaximumLevel(skillType))
            {
                MaximumSkillList.Add(skillType);
                CanEvoDict.Add(skillType, false);
            }
        }
    }

    public void OnSelectPassiveType(PassiveType passiveType, byte skillGradeIndex)
    {
        if (!ChosenPassiveTypeDict.ContainsKey(passiveType))
        {
            ChosenPassiveTypeDict.Add(passiveType, skillGradeIndex);
            maxPassiveSkillCount += 1;
        }
        else
        {
            ChosenPassiveTypeDict[passiveType] += 1;
            if (HasPassiveMaximumLevel(passiveType))
            {
                MaximumPassiveList.Add(passiveType);
            }
        }
    }

    public bool HasActiveSkillMaximumLevel(SkillType type)
    {
        if (ChosenSkillTypeDict[type] >= 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool HasPassiveMaximumLevel(PassiveType type)
    {
        if (ChosenPassiveTypeDict[type] >= 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
//note 
/*RandomNoRepeat<PassiveType> randomIndex;
List<PassiveType> selectedPassives = new List<PassiveType>(); // Danh sách tạm thời để lưu trữ các phần tử đã chọn

for (int i = 0; i < count; i++)
{
    if (UnownedPassiveSkill.Count == 0) break;

    if (HasReachedMaxPassiveSkills())
    {
        if (MaximumPassiveList.Count == 0) break; // Nếu MaximumPassiveList trống thì thoát khỏi vòng lặp
        randomIndex = new RandomNoRepeat<PassiveType>(MaximumPassiveList);
    }
    else
    {
        randomIndex = new RandomNoRepeat<PassiveType>(UnownedPassiveSkill);
    }

    PassiveType selectedPassive;

    do
    {
        selectedPassive = randomIndex.Random();
    } while (selectedPassives.Contains(selectedPassive)); // Đảm bảo không chọn trùng lặp

    UnityEngine.Debug.Log("Passive: " + selectedPassive);

    selectedPassives.Add(selectedPassive);
    MaximumPassiveList.Add(selectedPassive);

    AddValuePassiveSkill(selectedPassive);
}*/