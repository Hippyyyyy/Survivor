using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Durian : MonoBehaviour
{
    [SerializeField]
    public SpriteRenderer SpriteRenderer;

    [SerializeField]
    private Vector3 RotateVelocity;

    [NonSerialized]
    public bool IsPauseGame;

    private float CurrentVelocityValue;

    private Vector3 CurrentVelocity;

    private float DurianRadius;

    private float WidthBorder;

    private float HeightBorder;

    private Transform CameraTransform;

    private BorderData BorderData;

    private void Start()
    {
        Shoot(5f, Vector2.one * 1.5f);
    }

    public void Shoot(float velocity, Vector2 aoeMultiplier)
    {
        CurrentVelocityValue = velocity;
        SetRandomDirection();
        ChangeSize(aoeMultiplier);
    }

    public void ChangeVelocity(float newVelocity)
    {
        CurrentVelocityValue = newVelocity;
        CurrentVelocity = CurrentVelocity.normalized * CurrentVelocityValue;
    }

    public void ChangeSize(Vector2 aoeMultiplier)
    {
        transform.localScale = new Vector3(aoeMultiplier.x, aoeMultiplier.y, 1);
        DurianRadius = Mathf.Max(aoeMultiplier.x, aoeMultiplier.y) / 2;
    }

    private void SetRandomDirection()
    {
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        CurrentVelocity = new Vector3(randomDirection.x, randomDirection.y, 0) * CurrentVelocityValue;
    }

    public void Update()
    {
        if (IsPauseGame) return;

        transform.position += CurrentVelocity * Time.deltaTime;
        CalculateCollisionWithBorder();
        RotateDurian();
    }

    private void RotateDurian()
    {
        transform.Rotate(RotateVelocity * Time.deltaTime);
    }

    public void SetBorder(float widthBorder, float heightBorder, Transform cameraTransform)
    {
        WidthBorder = widthBorder;
        HeightBorder = heightBorder;
        CameraTransform = cameraTransform;
    }

    public void SetSize(float durianRadius)
    {
        transform.localScale = Vector3.one * durianRadius;
        DurianRadius = durianRadius / 2;
    }

    public void SetFixBorder(BorderData borderData)
    {
        BorderData = borderData;
    }

    private void CalculateCollisionWithBorder()
    {
        Vector3 position = transform.position;
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(position);

        if (viewportPosition.x <= 0 || viewportPosition.x >= 1)
        {
            CurrentVelocity.x = -CurrentVelocity.x;
        }

        if (viewportPosition.y <= 0 || viewportPosition.y >= 1)
        {
            CurrentVelocity.y = -CurrentVelocity.y;
        }

        transform.position = position;
    }

}
