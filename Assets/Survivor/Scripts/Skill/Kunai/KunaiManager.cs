using SCN.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiManager : BaseActiveSkill
{
    [SerializeField] AudioClip shootSound_Normal;

    [SerializeField] AudioClip shootSound_Evo;

    [SerializeField] List<int> listKunaiShoot;

    [SerializeField] List<Sprite> listImgKunai;

    [SerializeField] Kunai KunaiPrefab;

    [SerializeField] LayerMask enemyMask;

    List<Kunai> KunaiList;

    SkillLoader skillLoader;

    ObjectPool kunaiPool;

    Player player; 
    
    float PassiveSpeed;

    float PassiveDmg;

    float PassiveReduceCD;

    [SerializeField]int currentSkillLevel = 0;

    protected override void Awake()
    {
        base.Awake();
        KunaiList = new List<Kunai>();
        kunaiPool = new ObjectPool(KunaiPrefab.gameObject, transform);
    }
    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        SetSkillLevel(0);
    }

    protected override void Update()
    {
        
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void TriggerSkill()
    {

    }
    private IEnumerator ShootKunaiCoroutine()
    {
        while (true)
        {
            StartCoroutine(ShootKunai());
            yield return new WaitForSeconds(cooldown[currentSkillLevel]);
        }
    }

    IEnumerator ShootKunai()
    {
        int kunaiCount = listKunaiShoot[currentSkillLevel];
        EnemyBase closestEnemy = GetClosestEnemy();

        if (closestEnemy)
        {
            Vector2 targetPosition = closestEnemy.transform.position;

            for (int i = 0; i < kunaiCount; i++)
            {
                GameObject kunaiObj = kunaiPool.GetObjInPool();
                Kunai kunai = kunaiObj.GetComponent<Kunai>();
                kunai.SetPool(kunaiPool);
                kunai.transform.position = player.transform.position;
                kunai.gameObject.SetActive(true);
                kunai.Shoot(targetPosition, Vector3.one * bulletSize[currentSkillLevel], (int)duration[currentSkillLevel], true, listImgKunai[currentSkillLevel], PassiveSpeed, PassiveDmg);
                KunaiList.Add(kunai);

                yield return new WaitForSeconds(0.05f);
            }
        }
    }


    EnemyBase GetClosestEnemy()
    {
        EnemyBase closesEnemy = null;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(player.transform.position, range[0], enemyMask);
        if (enemies.Length <= 0)
            return null;

        float minDis = 10;

        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyBase enemyChecked = enemies[i].GetComponent<EnemyBase>();
            float disEnemy = Vector2.Distance(player.transform.position, enemyChecked.transform.position);

            if (disEnemy < minDis)
            {
                closesEnemy = enemyChecked;
                minDis = disEnemy;
            }
        }
        
        return closesEnemy;
    }
    protected override void SetCooldown()
    {
        
    }
    public void SetBulletSpeed(float newValue)
    {
        PassiveSpeed = newValue;
    }
    public void SetBulletDmg(float newValue)
    {
        PassiveDmg = newValue;
    }

    public void SetCooldownReduction(float newValue)
    {
        PassiveReduceCD = newValue;
    }

    public override void SetSkillLevel(int skillLevel)
    {
        SetBulletSpeed(projectilesFightSpeed[skillLevel]);
        SetCooldownReduction(cooldown[skillLevel]);
        SetCooldownReduction(damageMultiplier[skillLevel]);
        StopCoroutine(ShootKunaiCoroutine());
        currentSkillLevel = skillLevel;
        StartCoroutine(ShootKunaiCoroutine());

    }
    
    protected override void DealDamage()
    {
        
    }
}

