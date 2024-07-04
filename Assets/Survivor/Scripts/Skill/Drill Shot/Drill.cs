using System;
using UnityEngine;

public class Drill : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer SpriteRenderer;

    [SerializeField]
    private LineRenderer Tail;

    [NonSerialized]
    public int DrillIndex;

    [NonSerialized]
    public bool IsPauseGame;

    private float CurrentVelocityValue;

    private Vector3 CurrentVelocity;

    private int RemainTime;

    private float WidthBorder;

    private float HeightBorder;

    private Transform CameraTransform;

    private BorderData BorderData;

    private void Start()
    {
        Shoot(10f, Vector3.one, 5);
        SetBorder(10f, 10f, CameraTransform);
    }

    public void Shoot(float velocity, Vector3 scaleSizeMultiplier, int duration)
    {
        CurrentVelocityValue = velocity;
        transform.localScale = scaleSizeMultiplier;
        RemainTime = duration;
        SetDrillDirection();
    }

    private void Update()
    {
        if (IsPauseGame) return;

        transform.position += CurrentVelocity * Time.deltaTime;
        transform.up = CurrentVelocity;
        CalculateCollisionWithBorder();

    }

    private void SetDrillDirection()
    {
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        CurrentVelocity = new Vector3(randomDirection.x, randomDirection.y, 0) * CurrentVelocityValue;
    }

    public void SetBorder(float widthBorder, float heightBorder, Transform cameraTransform)
    {
        WidthBorder = widthBorder;
        HeightBorder = heightBorder;
        CameraTransform = cameraTransform;
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

    private void CalculateCollisionWithFixBorder()
    {
        // Implement fixed border collision logic
    }
}
