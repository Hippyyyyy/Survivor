using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCN.Effect;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    Player player;

    [Header("Settings")]
    [SerializeField] float moveSpd;

    private void Start()
    {
       
    }

    private void Update()
    {
       
    }

    public void StorePlayer(Player player)
    {
        this.player = player;

    }
    public void SetSpeed(float speed)
    {
        this.moveSpd = speed;
    }


    public void FollowPlayer()
    {
        if (!player)
            return;

        Vector2 dir = (player.transform.position - transform.position).normalized;

        Vector2 targetPos = (Vector2)transform.position + dir * moveSpd * Time.deltaTime;

        transform.position = targetPos;
    }

}
