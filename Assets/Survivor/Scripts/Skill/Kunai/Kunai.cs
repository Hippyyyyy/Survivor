using SCN.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D collider2D;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private SpriteRenderer TailObject;
    [NonSerialized] public int KunaiIndex;
    [NonSerialized] public bool IsPauseGame;
    EnemyBase targetEnemy;

    ObjectPool kunaiPool;

    private int RemainTime;
    private int TailFrameCount;
    float damage;

    public void Shoot(Vector2 targetPosition, Vector3 scaleSizeMultiplier, int duration, bool hasTail, Sprite kunaiSprite, float spd, float dmg)
    {
        SpriteRenderer.sprite = kunaiSprite;
        transform.localScale = scaleSizeMultiplier;
        collider2D.enabled = true;
        damage = dmg;

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * spd;
        transform.right = direction;
        RemainTime = duration;
        TailObject.gameObject.SetActive(hasTail);
        TailFrameCount = hasTail ? duration : 0;
    }

    private void OnEnable()
    {
        collider2D.enabled = true;
    }

    private void Update()
    {
        if (!IsPauseGame)
        {
            /*rb.velocity = CurrentVelocity * Time.deltaTime;*/

            RemainTime -= Mathf.RoundToInt(Time.deltaTime);

            UpdateTail();
        }
    }

    private void UpdateTail()
    {
        TailFrameCount--;

        if (TailFrameCount <= 0)
        {
            TailObject.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetEnemy)
            return;

        if (collision.TryGetComponent(out EnemyBase enemy))
        {
            targetEnemy = collision.GetComponent<EnemyBase>();
            ApplyDamage(enemy, damage);
            targetEnemy = null;
            RemoveObj();
        }
    }
    public void ApplyDamage(EnemyBase enemy, float dmg)
    {
        enemy.TakeDamage(dmg, false);
    }

    public void SetPool(ObjectPool pool)
    {
        this.kunaiPool = pool;
    }
    public void RemoveObj()
    {
        gameObject.SetActive(false);
        collider2D.enabled = false;
        kunaiPool.RemoveObj(gameObject);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!collider2D) collider2D = GetComponent<Collider2D>();
    }

    
#endif
}
