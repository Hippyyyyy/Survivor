using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] PlayerLevel playerLevel;
    [SerializeField] CircleCollider2D collider2D;

    public static Player Ins;

    private void Awake()
    {
        if (!Ins)
        {
            Ins = this;
        }
    }

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void TakeDamage(float damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public bool HasLevelUp()
    {
        Debug.Log(playerLevel.HasLevelUp());
        return playerLevel.HasLevelUp();
    }
}
