using SCN.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] Candy candyPrefab;
    [SerializeField] Cash cashPrefab;
    [SerializeField] [Range(0, 100)] int rateSpawn;

    ObjectPool candyPool;
    ObjectPool cashPool;

    private void Awake()
    {
        EnemyBase.onDead += EnemyDeadCallBack;
    }
    private void Start()
    {
        candyPool = new ObjectPool(candyPrefab.gameObject, transform);
        cashPool = new ObjectPool(cashPrefab.gameObject, transform);
    }

    private void OnDestroy()
    {
        EnemyBase.onDamageTaken = null;
    }
    private void EnemyDeadCallBack(Vector2 enemyPosition)
    {
        bool shoundSpawnCash = UnityEngine.Random.Range(0, 101) <= rateSpawn;
        if (!shoundSpawnCash)
        {
            SpawnCandy(enemyPosition);
        }
        else
        {
            SpawnCash(enemyPosition);
        }

    }

    void SpawnCandy(Vector2 enemyPosition)
    {
        var candy = candyPool.GetObjInPool();
        candy.transform.position = enemyPosition;
        candy.gameObject.SetActive(true);
        candy.GetComponent<Candy>().SetPool(candyPool);
    }
    void SpawnCash(Vector2 enemyPosition)
    {
        var cash = cashPool.GetObjInPool();
        cash.transform.position = enemyPosition;
        cash.gameObject.SetActive(true);
        cash.GetComponent<Cash>().SetPool(cashPool);
    }

}
