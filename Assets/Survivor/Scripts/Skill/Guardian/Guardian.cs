using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D circleCollider2d;

    public CircleCollider2D CircleCollider2d { get => circleCollider2d; set => circleCollider2d = value; }


    private void Awake()
    {
        circleCollider2d = GetComponent<CircleCollider2D>();
    }
}
