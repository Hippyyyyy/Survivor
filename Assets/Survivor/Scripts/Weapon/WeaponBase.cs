using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float range;
    [SerializeField] protected LayerMask enemyMask;

    [Header("Animations")]
    [SerializeField] protected float aimLerp;

    [Header("Attack")]
    [SerializeField] protected int damage;
    [SerializeField] protected float attackTimer;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected Animator animator;

    private void Start()
    {

    }

    private void Update()
    {
       
    }

    protected EnemyBase GetClosestEnemy()
    {
        EnemyBase closesEnemy = null;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);
        if (enemies.Length <= 0)
            return null;

        float minDis = range;

        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyBase enemyChecked = enemies[i].GetComponent<EnemyBase>();
            float disEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);

            if (disEnemy < minDis)
            {
                closesEnemy = enemyChecked;
                minDis = disEnemy;
            }
        }
        return closesEnemy;
    }

    protected int GetDamage(out bool isCriticalHit)
    {
        isCriticalHit = false;
        if (Random.Range(0, 101) <= 50)
        {
            isCriticalHit = true;
            return damage * 2;
        }
        return damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
