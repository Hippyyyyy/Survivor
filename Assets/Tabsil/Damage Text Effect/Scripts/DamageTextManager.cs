using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private DamageText damageTextPrefab;

    [Header(" Pooling ")]
    private ObjectPool<DamageText> damageTextPool;

    private void Awake()
    {
        EnemyBase.onDamageTaken += EnemyTakenDamageCallback;
    }

    private void OnDestroy()
    {
        EnemyBase.onDamageTaken -= EnemyTakenDamageCallback;    
    }

    private void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private void EnemyTakenDamageCallback(Vector2 pos, int value, bool isCriticalHit)
    {
        DamageText damageTextInstance = damageTextPool.Get();

        damageTextInstance.transform.position = pos;
        damageTextInstance.Configure(value.ToString(), isCriticalHit);

        LeanTween.delayedCall(1, () => damageTextPool.Release(damageTextInstance));
    }

    private DamageText CreateFunction()
    {
        return Instantiate(damageTextPrefab, transform);
    }

    private void ActionOnGet(DamageText bonusParticle)
    {
        bonusParticle.gameObject.SetActive(true);
    }

    private void ActionOnRelease(DamageText bonusParticle)
    {
        bonusParticle.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(DamageText bonusParticle)
    {
        Destroy(bonusParticle);
    }

    [ContextMenu("Test Damage")]
    private void Test()
    {
       
    }
}
