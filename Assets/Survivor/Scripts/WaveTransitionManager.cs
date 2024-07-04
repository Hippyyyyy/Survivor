using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [SerializeField] Button[] upgradeContainers;



    
    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WAVETRANSITION:
                ConfigUpgradeContainers();
                break;
        }
    }

    void ConfigUpgradeContainers()
    {
        for (int i = 0; i < upgradeContainers.Length; i++)
        {
            upgradeContainers[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Upgrade" + i;
        }
    }
}
