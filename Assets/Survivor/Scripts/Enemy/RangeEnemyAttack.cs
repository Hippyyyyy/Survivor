using SCN.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangeEnemyAttack : MonoBehaviour
{
    [Header("Elements")]
    Player player;
    [SerializeField] Transform shootingPoint;
    [SerializeField] EnemyBullet bulletPrefab;

    [Header("Attack")]
    [SerializeField] int damage;
    [SerializeField] float spd;
    [SerializeField] float attackFrequency;
    float attackDelay;
    float attackTimer;

    ObjectPool bulletPool;

    private void Start()
    {
        bulletPool = new ObjectPool(bulletPrefab.gameObject, shootingPoint);
        
        attackDelay = 1 / attackFrequency;
        attackTimer = attackDelay;

    }

    public void StorePlayer(Player player)
    {
        this.player = player;
    }

    public void AutoAim()
    {
        ManagerAttack();
    }

    public void ManagerAttack()
    {
        attackTimer += Time.deltaTime;
        if (attackDelay <= attackTimer)
        {
            attackTimer = 0;
            Shoot();
        }
    }
    
    void Shoot()
    {
        Vector2 dir = (player.transform.position - transform.position).normalized;
        GameObject pool = bulletPool.GetObjInPool();
        EnemyBullet bullet = pool.GetComponent<EnemyBullet>();
        if (bullet)
        {
            bullet.SetUp(shootingPoint);
            bullet.Shoot(damage, dir);
            bullet.SetPool(bulletPool);
        }
        //gizmoDir = dir;
    }

    public void RemoveBullet(GameObject obj)
    {
        bulletPool.RemoveObj(obj);
    }

    //Vector2 gizmoDir;
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(shootingPoint.position, (Vector2)shootingPoint.position + gizmoDir * 5);
    }*/
}
