using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    protected EnemyMovement movement;

    [Header("Elements")]
    protected Player player;
    [SerializeField] protected SpriteRenderer renderer;
    [SerializeField] protected SpriteRenderer spawnIndicator;
    [SerializeField] protected Collider2D collider2D;

    [Header("Settings")]
    [SerializeField] protected float playerDetection;
    [SerializeField] protected bool isGizmo = false;

    [Header("Health")]
    protected float health;
    [SerializeField] protected float maxHealth;

    [Header("Effects")]
    [SerializeField] protected ParticleSystem par;

    

    [Header("Actions")]
    public static Action<Vector2, int, bool> onDamageTaken;
    public static Action<Vector2> onDead;

    protected void Update()
    {
        if (!renderer.enabled)
            return;
    }

    protected virtual void Start()
    {
        health = maxHealth;
        movement = GetComponent<EnemyMovement>();
        player = FindFirstObjectByType<Player>();
        StartSpawn();
    }

    void StartSpawn()
    {
        renderer.enabled = false;
        spawnIndicator.enabled = true;

        DOVirtual.DelayedCall(.3f, () =>
        {
            Vector2 targetScale = spawnIndicator.transform.localScale * 1.2f;
            spawnIndicator.transform.DOScale(targetScale, .3f).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
            {
                SpawnCompleted();
            });
        });
    }
    void SpawnCompleted()
    {
        SetRendererVisibility(true);
        collider2D.enabled = true;
        movement.StorePlayer(player);
        movement.SetSpeed(1);
    }
    void SetRendererVisibility(bool isVisibility)
    {
        renderer.enabled = isVisibility;
        spawnIndicator.enabled = !isVisibility;
    }

    public void TakeDamage(float damage, bool isCritticalHit)
    {
        int realDamage = (int)Mathf.Min(damage, health);
        health -= realDamage;

        //SetTextHealth();
        onDamageTaken?.Invoke(transform.position, (int)damage, isCritticalHit);

        if (health <= 0)
        {
            Dead();
        }
    }
    void Dead()
    {
        onDead?.Invoke(transform.position);
        par.transform.SetParent(null);
        par.Play();
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        if (!isGizmo) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetection);

    }

}
