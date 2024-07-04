using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;

    private void Awake()
    {
        if (!Ins)
        {
            Ins = this;
        }    
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        SetGameState(GameState.MENU);
    }

    public void StartGame()
    {
        SetGameState(GameState.GAME);
    }
    public void OpenShop()
    {
        SetGameState(GameState.SHOP);
    }
    public void StartWeaponSelection()
    {
        SetGameState(GameState.WEAPONSELECTION);
    }

    public void WaveCompleted()
    {
        if (Player.Ins.HasLevelUp())
        {
            SetGameState(GameState.WAVETRANSITION);
        }
        else
        {
            SetGameState(GameState.SHOP);
        }
    }

    public void SetGameState(GameState state)
    {
        IEnumerable<IGameStateListener> gameStateListeners = FindObjectsOfType<MonoBehaviour>().OfType<IGameStateListener>();
        foreach (IGameStateListener gameState in gameStateListeners)
        {
            gameState.GameStateChangedCallback(state);
        }

        if (state == GameState.GAMEOVER)
        {
            ManagerGameOver();
        }
    }

    public void ManagerGameOver()
    {
        DOVirtual.DelayedCall(2f, ()=> SceneManager.LoadScene(0));
    }
}
