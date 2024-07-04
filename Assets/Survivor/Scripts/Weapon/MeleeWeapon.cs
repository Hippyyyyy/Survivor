using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    enum State 
    {
        Idle, Attack
    }
    State state;

    [Header("Elements")]
    [SerializeField] Transform hitDetection;
    [SerializeField] BoxCollider2D hitCollider;

    [Header("Attack")]
    List<EnemyBase> damagedEnemies = new List<EnemyBase>();


    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;
            case State.Attack:
                Attacking();
                break;
        }

        
    }

    void AutoAim()
    {
        EnemyBase closesEnemy = GetClosestEnemy();
        Vector2 targetUpVector = Vector3.up;

        if (closesEnemy != null)
        {
            ManagerAttack();
            targetUpVector = (closesEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
        }    
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
        IncrementAttackTimer();
    }

    void ManagerAttack()
    {
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;
            StartAttack();
        }
    }

    void IncrementAttackTimer()
    {
        attackTimer += Time.deltaTime;
    }

    //[NaughtyAttributes.Button]
    void StartAttack()
    {
        animator.Play("attack");
        state = State.Attack;
        damagedEnemies.Clear();

        animator.speed = 1 / attackDelay;
    }

    void Attacking()
    {
        Attack();
    }
    void StopAttack()
    {
        state = State.Idle;
        damagedEnemies.Clear();
    }
    void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(
            hitDetection.position,
            hitCollider.bounds.size,
            hitDetection.localEulerAngles.z,
            enemyMask);

        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyBase enemy = enemies[i].GetComponent<EnemyBase>();
            if (!damagedEnemies.Contains(enemy))
            {
                int damage = GetDamage(out bool isCriticalHit);

                enemy.TakeDamage(damage, isCriticalHit);
                damagedEnemies.Add(enemy);
            }
        }
    }

}
