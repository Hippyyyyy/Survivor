using System;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Vector3 rotateSpeed;

    [NonSerialized]
    public bool isPauseGame;

    private Vector3 currentVelocity;

    private Vector3 accelerator;

    private float flyTimer;

    private int forwardDuration;

    private int backwardDuration;

    private bool isMovingForward;

    private void Start()
    {
        
    }

    public void Shoot(Vector2 velocity, Vector3 scaleSizeMultiplier, int flyDuration)
    {
        currentVelocity = velocity;
        transform.localScale = scaleSizeMultiplier;
        forwardDuration = flyDuration / 2;
        backwardDuration = flyDuration;
        flyTimer = Time.time;
        isMovingForward = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 velocity = new Vector2(5, 7) * 2;
            Vector3 scaleSizeMultiplier = Vector3.one * 0.7f;
            int flyDuration = 3;
            Shoot(velocity, scaleSizeMultiplier, flyDuration);
        }
        if (isPauseGame)
            return;

        float elapsedTime = Time.time - flyTimer;

        if (isMovingForward)
        {
            if (elapsedTime >= forwardDuration)
            {
                isMovingForward = false;
                flyTimer = Time.time;
                currentVelocity = -currentVelocity;
            }
        }
        else
        {
            if (elapsedTime >= backwardDuration)
            {
                currentVelocity = Vector3.zero;
                return;
            }
        }

        transform.position += currentVelocity * Time.deltaTime;
        transform.Rotate(rotateSpeed * Time.deltaTime);
    }
}
