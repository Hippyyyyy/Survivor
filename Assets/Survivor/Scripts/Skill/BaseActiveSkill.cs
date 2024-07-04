using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActiveSkill : MonoBehaviour, IDamageSkill
{
    [SerializeField] protected SkillType skillType;
    protected int id;
    [Header("Name")]
    protected string name;

    [Header("Mechanics")]
    [Tooltip("Mô tả về cơ chế hoạt động của kỹ năng")]
    protected string mechanics;

    [Header("Level")]
    [Tooltip("Cấp độ của kỹ năng")]
    protected List<int> level;

    [Header("Description")]
    [Tooltip("các mô tả về kỹ năng tại mỗi cấp độ")]
    protected List<string> description;

    [Header("Damage")]
    [Tooltip("Hệ số nhân dmg của skill")]
    protected List<float> damageMultiplier;

    [Header("Spread")]
    protected List<float> spread;

    [Header("Number Of Projectiles")]
    [Tooltip("Số lượng vật thể được phát ra bởi kỹ năng")]
    protected List<int> numberOfProjectiles;

    [Header("Fire Rate")]
    [Tooltip("Tốc độ bắn của skill")]
    protected List<float> fireRate;

    [Header("Cooldown")]
    [Tooltip("Cooldown theo cấp của skill")]
    protected List<float> cooldown;

    [Header("Speed Skill")]
    [Tooltip("Speed đạn của skill khi được bắn ra")]
    protected List<float> projectilesFightSpeed;

    [Header("Bullet Size")]
    [Tooltip("Size của đạn")]
    protected List<float> bulletSize;

    [Header("Area Size")]
    [Tooltip("Vùng ảnh hưởng của đạn")]
    protected List<float> areaSize;

    [Header("Range")]
    [Tooltip("Khoảng cách mà skill có thể bay đến được")]
    protected List<float> range;

    [Header("Duration")]
    [Tooltip("Duration cơ bản theo cấp của skill")]
    protected List<float> duration;

    [Header("Interval")]
    [Tooltip("Thời gian giữa các lần gây dmg")]
    protected List<float> interval;

    protected List<int> piercing;

    protected List<float> knockback;

    protected int prereqId;

    protected int evoId;

    protected string powerupType;

    SkillLoader skillLoader;

    protected virtual void Awake()
    {
        skillLoader = FindObjectOfType<SkillLoader>();
        LoadData();
    }

    protected virtual void OnDestroy()
    {

    }

    protected virtual void Update()
    {

    }
    void LoadData()
    {
        if (skillLoader != null && skillLoader.skillData != null)
        {
            var skill = skillLoader.skillData.FindActiveSkill(skillType);
            numberOfProjectiles = skill.number_of_projectiles;
            bulletSize = skill.bullet_size;
            cooldown = skill.cooldown;
            damageMultiplier = skill.damage_multiplier;
            level = skill.level;
            description = skill.description;
            damageMultiplier = skill.damage_multiplier;
            projectilesFightSpeed = skill.projectiles_fight_speed;
            spread = skill.spread;
            fireRate = skill.fire_rate;
            range = skill.range;
            duration = skill.duration;
            interval = skill.interval;
        }
    }
    protected abstract void DealDamage();
    protected abstract void TriggerSkill();
    public virtual void SetSkillLevel(int skillLevel)
    {
        
    }
    protected abstract void SetCooldown();

    public void SetDamage(float newValue)
    {

    }
}
