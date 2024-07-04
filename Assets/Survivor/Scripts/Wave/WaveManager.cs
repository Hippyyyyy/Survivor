using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour, IGameStateListener
{
    [SerializeField] Player player;
    [SerializeField] WaveManagerUI waveManagerUI;
    [SerializeField] Wave[] wave;
    List<float> localCounters = new List<float>();
    [SerializeField] float waveDuration;
    float timer;
    bool isTimeOn;
    int currentWaveIndex;

    void Awake()
    {
        waveManagerUI = GetComponent<WaveManagerUI>();
    }

    private void Update()
    {
        if (!isTimeOn)
            return;

        if (timer < waveDuration)
        {
            ManageCurrentWave();
            string time = ((int)(waveDuration - timer)).ToString(); 
            waveManagerUI.UpdateTextTimer(time);
        }
        else
        {
            StartWaveTransition();
        }
    }

    void StartWave(int waveIndex)
    {
        waveManagerUI.UpdateTextWave("Wave " + (currentWaveIndex + 1) + " / " + wave.Length);
        localCounters.Clear();
        foreach (var item in wave[waveIndex].waveSegments)
        {
            localCounters.Add(1);
        }
        timer = 0;
        isTimeOn = true;
    }

    void ManageCurrentWave()
    {
        Wave waveCurrent = wave[currentWaveIndex];

        for (int i = 0; i < waveCurrent.waveSegments.Count; i++)
        {
            WaveSegment segment = waveCurrent.waveSegments[i];
            float tStart = segment.tStartEnd.x / 100 * waveDuration;
            float tEnd = segment.tStartEnd.y / 100 * waveDuration;

            if (timer < tStart || timer > tEnd)
                continue;

            float timeSinceSegmentStart = timer - tStart;

            float spawnDelay = 1f / segment.spawnFrequency;

            if (timeSinceSegmentStart / spawnDelay > localCounters[i])
            {
                var obj = Instantiate(segment.prefab, transform);
                obj.transform.position = GetVectorPosition();
                localCounters[i]++;
            }
        }
        timer += Time.deltaTime;

    }

    void DefeatAllEnemies()
    {
        transform.Clear();
    }

    void StartNewWave()
    {
        StartWave(currentWaveIndex);
    }

    void StartWaveTransition()
    {
        isTimeOn = false;
        DefeatAllEnemies();
        currentWaveIndex++;
        if (currentWaveIndex >= wave.Length)
        {
            waveManagerUI.UpdateTextWave("Completed");
            waveManagerUI.UpdateTextTimer("");
            GameManager.Ins.SetGameState(GameState.STAGECOMPLETE);
        }
        else
        {
            GameManager.Ins.WaveCompleted();
        }
        
    }

    Vector2 GetVectorPosition()
    {
        Vector2 direction = Random.insideUnitSphere;
        Vector2 offset = direction.normalized * Random.Range(6, 10);
        Vector2 targetPos = (Vector2)player.transform.position + offset;

        targetPos.x = Mathf.Clamp(targetPos.x, -18, 18);
        targetPos.x = Mathf.Clamp(targetPos.y, -8, 8);

        return targetPos;
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                break;
            case GameState.GAME:
                StartNewWave();
                break;
            case GameState.GAMEOVER:
                isTimeOn = false;
                DefeatAllEnemies();
                break;
            
        }
    }

    
}
[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> waveSegments;
}

[System.Serializable]
public struct WaveSegment
{
    [MinMaxSlider(0, 100)] public Vector2 tStartEnd;
    public float spawnFrequency;
    public GameObject prefab;
}

