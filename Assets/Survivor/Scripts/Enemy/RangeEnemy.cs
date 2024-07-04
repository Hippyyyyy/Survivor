using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement), typeof(RangeEnemyAttack))]
public class RangeEnemy : EnemyBase
{
    [SerializeField] RangeEnemyAttack rangeEnemyAttack;

    protected override void Start()
    {
        base.Start();
        rangeEnemyAttack = GetComponent<RangeEnemyAttack>();
        rangeEnemyAttack.StorePlayer(player);
    }

    new void Update()
    {
        base.Update();
        ManagerAttack();

        renderer.transform.localScale = player.transform.position.x > transform.position.x ? Vector3.one : Vector3.one.With(x: -1);
    }

    public void ManagerAttack()
    {
        float disPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (disPlayer > playerDetection)
        {
            movement.FollowPlayer();
        }
        else
        {
            TryAttack();
        }
    }
   
    void TryAttack()
    {
        rangeEnemyAttack.AutoAim();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!collider2D) collider2D = GetComponent<Collider2D>();
    }
#endif
}
