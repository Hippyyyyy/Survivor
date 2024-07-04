using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header ("Elements")]
    Rigidbody2D rb;
    [SerializeField] MobileJoystick joystick;

    [Header("Settings")]
    [SerializeField] float moveSpd;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right;
    }
    private void FixedUpdate()
    {
        Vector2 moveVector = joystick.GetMoveVector();
        MovePlayer(moveVector);
    }
    private void MovePlayer(Vector3 moveVector)
    {
        rb.velocity = moveVector * moveSpd * Time.fixedDeltaTime;
    }
    
}
