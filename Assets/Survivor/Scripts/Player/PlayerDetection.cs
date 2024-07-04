using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] CircleCollider2D playerCollider;
    Player player;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            if (!collision.IsTouching(playerCollider))
            {
                return;
            }

            collectable.Collect(GetComponent<Player>());
        }
    }
}
