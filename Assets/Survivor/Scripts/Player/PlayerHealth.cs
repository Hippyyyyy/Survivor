using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    
    [Header("Settings")]
    [SerializeField] float maxHealth;
    [SerializeField] float health;

    [Header("UI")]
    [SerializeField] Slider sliderHealth;
    [SerializeField] TextMeshProUGUI textHealth;

    private void Start()
    {
        SetUp();
    }

    public void TakeDamage(float damage)
    {
        int realDamage = (int)Mathf.Min(damage, health);
        health -= realDamage;

        float healthValue = health / maxHealth;
        sliderHealth.value = healthValue;

        SetTextHealth();

        if (health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        GameManager.Ins.SetGameState(GameState.GAMEOVER);
    }

    void SetUp()
    {
        health = maxHealth;
        sliderHealth.value = 1;
        SetTextHealth();
        SetTextHealth();
    }

    public void SetTextHealth()
    {
        textHealth.text = health + " / " + maxHealth;
    }
}
