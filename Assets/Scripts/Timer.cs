using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float seconds = 0;

    private float minutes = 0;

    private bool timerOn = false;

    public float besttime = 0;

    [SerializeField] private TextMeshProUGUI timerText;
    
    void Update()
    {
        if (timerOn)
        {
            seconds += Time.deltaTime;
            if (seconds >= 60)
            {
                minutes += 1;
                seconds -= 60;
            }

            if (seconds >= 10)
                timerText.text = minutes + ":" + (int)seconds;
            else
                timerText.text = minutes + ":0" + (int)seconds;
        }
    }

    public void StartTimer()
    {
        timerOn = true;
        seconds = 0;
        minutes = 0;
    }

    public void StopTimer()
    {
        if (besttime == 0 || besttime > seconds + minutes * 60)
        {
            besttime = seconds + minutes * 60;
        }
        timerOn = false;
    }
}
