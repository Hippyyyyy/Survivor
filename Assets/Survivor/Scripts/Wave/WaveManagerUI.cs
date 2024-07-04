using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManagerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textWave;
    [SerializeField] TextMeshProUGUI textTimer;

    public void UpdateTextWave(string wave)
    {
        textWave.text = wave;
    }
    public void UpdateTextTimer(string timer)
    {
        textTimer.text = timer;
    }
}
