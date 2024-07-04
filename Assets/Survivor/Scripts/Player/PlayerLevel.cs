using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [Header("Settings")]
    float requiredXP;
    float currentXP;
    int level = 1;

    [Header("UI")]
    [SerializeField] Slider levelSlider;
    [SerializeField] TextMeshProUGUI textLv;
    int levelsEarnedThisWave;
    bool isDebug;
    private void Start()
    {
        UpdateRequiredXP();
        SetUpUI();
    }

    private void Awake()
    {
        Candy.onCollect += OnCollectCallBack;
    }
    private void OnEnable()
    {
        Candy.onCollect += OnCollectCallBack;
    }
    private void OnDisable()
    {
        Candy.onCollect = null;
    }
    private void OnDestroy()
    {
        Candy.onCollect = null;
    }

    void UpdateRequiredXP()
    {
        requiredXP = (level + 1) * 5;
    }

    void SetUpUI()
    {
        levelSlider.value = (float)currentXP / requiredXP;
        textLv.text = "Level " + (level + 1);
    }
    void OnCollectCallBack()
    {
        currentXP++;
        if (currentXP >= requiredXP)
        {
            LevelUp();
        }
        SetUpUI();
    }

    private void LevelUp()
    {
        level++;
        levelsEarnedThisWave++;
        currentXP = 0;
        UpdateRequiredXP();
    }

    public bool HasLevelUp()
    {
        if (isDebug)
            return true;

        if (levelsEarnedThisWave > 0)
        {
            levelsEarnedThisWave--;
            return true;
        }
        return false;
    }

}
