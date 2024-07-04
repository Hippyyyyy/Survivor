using SCN.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] int damage;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] BoxCollider2D boxCollider2D;
    ObjectPool bulletPool;

    [Header("Settings")]
    [SerializeField] float moveSpd;

    public void SetPool(ObjectPool pool)
    {
        this.bulletPool = pool;
    }

    public void Shoot(int damage, Vector2 direction)
    {
        this.damage = damage;
        transform.right = direction;
        rb.velocity = direction * moveSpd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Debug.Log("check");
            player.TakeDamage(damage);
            bulletPool.RemoveObj(gameObject);
            boxCollider2D.enabled = false;
            gameObject.SetActive(false);
        }
    }

    public void SetUp(Transform direction)
    {
        boxCollider2D.enabled = true;
        transform.position = direction.position;
        gameObject.SetActive(true);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!boxCollider2D) boxCollider2D = GetComponent<BoxCollider2D>();
    }
#endif
}
