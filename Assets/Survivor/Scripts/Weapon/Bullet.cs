using SCN.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] int damage;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D collider2D;
    ObjectPool bulletPool;

    [Header("Settings")]
    [SerializeField] float moveSpd;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] bool isCriticalHit;

    EnemyBase targetEnemy;

    public void SetPool(ObjectPool pool)
    {
        this.bulletPool = pool;
    }
    public void SetUp(Transform direction)
    {
        collider2D.enabled = true;
        transform.position = direction.position;
        gameObject.SetActive(true);
    }

    public void Shoot(int damage, Vector2 direction, bool isCriticalHit)
    {
        this.damage = damage;
        this.isCriticalHit = isCriticalHit;
        //transform.right = direction;
        rb.velocity = direction * moveSpd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetEnemy)
            return;

        if (IsLayerMask(collision.gameObject.layer, enemyMask))
        {
            targetEnemy = collision.GetComponent<EnemyBase>();
            Attack(targetEnemy);
            collider2D.enabled = false;
            targetEnemy = null;
            gameObject.SetActive(false);
        }
    }

    void Attack(EnemyBase enemy)
    {
        enemy.TakeDamage(damage, isCriticalHit);
    }

    bool IsLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!collider2D) collider2D = GetComponent<Collider2D>();
    }
#endif
}
