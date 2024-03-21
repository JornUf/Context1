using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class prompt_blink : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] float cycleTime;
    [SerializeField] float openTime;
    

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartBlinking();
    }

    public void StartBlinking()
    {
        InvokeRepeating(nameof(Blink), 1, cycleTime);
    }
    void Blink()
    {
        text.enabled = true;
        Invoke(nameof(Unblink), openTime);
    }

    void Unblink()
    {
        text.enabled = false;
    }

    public void StopBlinking()
    {
        text.enabled = false;
        CancelInvoke();
    }

   
}
