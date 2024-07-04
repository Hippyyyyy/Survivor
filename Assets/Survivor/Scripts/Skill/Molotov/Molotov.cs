using System;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    [SerializeField]
    private AudioClip ExplodeSound;

    [SerializeField]
    private SpriteRenderer BottleSpriteRenderer;

    [SerializeField]
    private SpriteRenderer FlameSpriteRenderer;

    [SerializeField]
    private SpriteRenderer ExplodeSpriteRenderer;

    [SerializeField]
    private Vector3 RotateSpeed;

    [NonSerialized]
    public int FlameIndex;

    [NonSerialized]
    public bool IsPauseGame;

    private Vector3 CurrentVelocity;

    private Vector3 FlameVelocity;

    private float Timer;

    private float BurnDuration;

    private float ThrowTime;

    private int FrameIndex;

    private Vector2 AOEMultiplier;

    public bool IsThrowing { get; private set; }

    public void ThrowMolotov(float throwSpeed, float throwTime, Vector2 aoeMultiplier, float duration, Vector2 throwVelocityNormalize, Vector2 flameVelocity)
    {
        CurrentVelocity = throwVelocityNormalize * throwSpeed;
        FlameVelocity = flameVelocity;
        ThrowTime = throwTime;
        AOEMultiplier = aoeMultiplier;
        BurnDuration = duration;
        IsThrowing = true;
        Timer = 0f;

        EnableBottle();
    }

    private void Update()
    {
        if (IsPauseGame || !IsThrowing)
            return;

        Timer += Time.deltaTime;

        if (Timer >= ThrowTime)
        {
            EnableExplode();
            IsThrowing = false;
        }
        else
        {
            transform.position += CurrentVelocity * Time.deltaTime;
            transform.Rotate(RotateSpeed * Time.deltaTime);
        }
    }

    private void EnableBottle()
    {
        BottleSpriteRenderer.enabled = true;
        FlameSpriteRenderer.enabled = false;
        ExplodeSpriteRenderer.enabled = false;
    }

    private void EnableExplode()
    {
        BottleSpriteRenderer.enabled = false;
        FlameSpriteRenderer.enabled = false;
        ExplodeSpriteRenderer.enabled = true;

        AudioSource.PlayClipAtPoint(ExplodeSound, transform.position);

    }

    private void EnableFlame()
    {
        BottleSpriteRenderer.enabled = false;
        FlameSpriteRenderer.enabled = true;
        ExplodeSpriteRenderer.enabled = false;
    }

}
