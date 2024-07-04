using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameStateListener
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject game;
    [SerializeField] GameObject waveTransition;
    [SerializeField] GameObject gameover;
    [SerializeField] GameObject stageComplete;
    [SerializeField] GameObject weaponSelection;
    List<GameObject> listPanel = new List<GameObject>();

    private void Awake()
    {

        listPanel.AddRange(new[]{
            menu,
            shop,
            game,
            waveTransition,
            gameover,
            stageComplete,
            weaponSelection
        });
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                ShowPanel(menu);
                break;
            case GameState.GAME:
                ShowPanel(game);
                break;
            case GameState.SHOP:
                ShowPanel(shop);
                break;
            case GameState.WAVETRANSITION:
                ShowPanel(waveTransition);
                break;
            case GameState.GAMEOVER:
                ShowPanel(gameover);
                break;
            case GameState.STAGECOMPLETE:
                ShowPanel(stageComplete);
                break;
            case GameState.WEAPONSELECTION:
                ShowPanel(weaponSelection);
                break;

        }
    }
    void ShowPanel(GameObject obj)
    {
        foreach (var item in listPanel)
        {
            item.gameObject.SetActive(item == obj);
        }
    }
}
