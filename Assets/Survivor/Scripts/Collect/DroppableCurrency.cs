using SCN.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DroppableCurrency : MonoBehaviour, ICollectable
{
    ObjectPool pool;
    bool isCollected;
    [SerializeField] protected CircleCollider2D circleCollider;
    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        isCollected = false;
    }

    public void Collect(Player player)
    {
        if (isCollected)
        {
            return;
        }

        isCollected = true;

        StartCoroutine(MoveTowardsPlayer(player));
    }

    IEnumerator MoveTowardsPlayer(Player player)
    {
        float timer = 0;
        Vector2 initialPositon = transform.position;

        while (timer < 1f)
        {
            transform.position = Vector2.Lerp(initialPositon, player.transform.position, timer);
            timer += Time.deltaTime;
            yield return null;
        }


        Collected();
    }

    public void SetPool(ObjectPool pool)
    {
        this.pool = pool;
        circleCollider.enabled = true;
    }
    public void RemoveObj()
    {
        gameObject.SetActive(false);
        circleCollider.enabled = false;
        pool.RemoveObj(gameObject);
    }

    protected abstract void Collected();
    
}
