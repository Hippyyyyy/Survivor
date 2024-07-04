using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer RocketGO;

    [SerializeField]
    private SpriteRenderer ExplosionGO;

    [NonSerialized]
    public int RocketIndex;

    [NonSerialized]
    public bool IsPauseGame;

    private Vector3 CurrentVelocity;

    private float FlyTime;

    private float Timer;

    private RocketManager RocketManager;

    public void Shoot(Vector2 velocity, float flyTime, Vector2 aoeMultiplier, RocketManager rocketManager)
    {
        CurrentVelocity = velocity;
        FlyTime = flyTime;
        transform.localScale = aoeMultiplier;
        RocketManager = rocketManager;
        SetRocketDirection();
    }

    public void DealExplosionDamage()
    {

    }

    public void Update()
    {
        if (IsPauseGame)
            return;

        Timer += Time.deltaTime;

        if (Timer >= FlyTime)
        {

        }
        else
        {
            transform.position += CurrentVelocity * Time.deltaTime;
            SetRocketDirection();
        }
    }

    private void SetRocketDirection()
    {
        transform.up = CurrentVelocity;
    }
}
