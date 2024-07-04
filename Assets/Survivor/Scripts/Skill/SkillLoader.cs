using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

public class SkillLoader : MonoBehaviour
{
    public string jsonFilePath = "Assets/skill2.json";
    public SkillData skillData;
    public static SkillLoader Ins;

    private void Awake()
    {
        if (!Ins)
        {
            Ins = this;
        }
    }

    void Start()
    {
        
    }

    void LoadSkillData()
    {
        if (File.Exists(jsonFilePath))
        {
            try
            {
                string json = File.ReadAllText(jsonFilePath);
                Debug.Log("JSON content: " + json);
                skillData = JsonUtility.FromJson<SkillData>(json);
                Debug.Log("Skills Loaded: " + skillData.active_skill.Count);
                Debug.Log("Passives Loaded: " + skillData.passive_effects.Count);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to parse JSON file: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("Cannot find the JSON file at " + jsonFilePath);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadSkillData();
    }
#endif
}
[Serializable]
public class ActiveSkill
{
    public int id;
    public string name;
    public string mechanics;
    public List<int> level;
    public List<string> description;
    public List<float> damage_multiplier;
    public List<float> spread;
    public List<int> number_of_projectiles;
    public List<float> fire_rate;
    public List<float> cooldown;
    public List<float> projectiles_fight_speed;
    public List<float> bullet_size;
    public List<float> area_size;
    public List<float> range;
    public List<float> duration;
    public List<float> interval;
    public List<int> piercing;
    public List<float> knockback;
    public int prereqId;
    public int evoId;
    public string powerupType;
}
[Serializable]
public class PassiveEffects
{
    public int id;
    public string name;
    public string mechanics;
    public List<int> level;
    public List<string> description;
    public string powerupType;
}
[Serializable]
public class EvoSkill
{
    public int id;
    public string name;
    public string mechanics;
    public int combine;
    public string description;
    public float damage_multiplier;
    public float spread;
    public int number_of_projectiles;
    public float fire_rate;
    public float cooldown;
    public float projectiles_fight_speed;
    public float bullet_size;
    public float area_size;
    public float range;
    public float duration;
    public float interval;
    public int piercing;
    public float knockback;
    public string powerupType;
}
[Serializable]
public class SkillData
{
    public List<ActiveSkill> active_skill;
    public List<PassiveEffects> passive_effects;

    public ActiveSkill FindActiveSkill(SkillType type)
    {
        return active_skill.Find(x => x.id == (int)type);
    }
    public PassiveEffects FindPassiveSkill(PassiveType type)
    {
        return passive_effects.Find(x => x.id == (int)type);
    }
}


