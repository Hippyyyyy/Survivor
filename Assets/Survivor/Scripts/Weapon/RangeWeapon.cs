using SCN.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : WeaponBase
{
    [Header("Elements")]
    [SerializeField] Transform shootingPoint;
    [SerializeField] Bullet bulletPrefab;
    ObjectPool bulletPool;

    private void Update()
    {
        AutoAim();
        bulletPool = new ObjectPool(bulletPrefab.gameObject, transform.parent.parent.parent);
    }

    void AutoAim()
    {
        EnemyBase closesEnemy = GetClosestEnemy();
        Vector2 targetUpVector = Vector3.up;

        if (closesEnemy != null)
        {
            targetUpVector = (closesEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
            ManageShooting();
        }
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
    }

    void ManageShooting()
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
        int damage = GetDamage(out bool isCriticalHit);
        Vector2 dir = (GetClosestEnemy().transform.position - transform.position).normalized;
        GameObject pool = bulletPool.GetObjInPool();
        Bullet bullet = pool.GetComponent<Bullet>();
        if (bullet)
        {
            bullet.SetUp(shootingPoint);
            bullet.Shoot(damage, transform.up, isCriticalHit);
            bullet.SetPool(bulletPool);
        }
        //gizmoDir = dir;
    }

}
