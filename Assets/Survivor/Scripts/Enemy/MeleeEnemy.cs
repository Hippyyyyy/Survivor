using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class MeleeEnemy : EnemyBase
{
    [Header("Attack")]
    [SerializeField] float damage;
    [SerializeField] float attackFrequency;
    float attackDelay;
    float attackTimer;

    
    protected override void Start()
    {
        base.Start();
        attackDelay = 1 / attackFrequency;
    }

    new void Update()
    {
        base.Update();
        if (attackTimer >= attackDelay)
        {
            TryAttack();
        }
        else
        {
            Wait();
        }
        
        movement.FollowPlayer();
    }

    void TryAttack()
    {
        float disPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (disPlayer <= playerDetection)
        {
            Attack();
        }
    }

    void Attack()
    {
        attackTimer = 0;

        player.TakeDamage(damage);
    }


    void Wait()
    {
        attackTimer += Time.deltaTime;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!collider2D) collider2D = GetComponent<Collider2D>();
    }
#endif
}
