using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            _timer.StopTimer();
            other.gameObject.GetComponent<PlayerController>().DieOrWin();
        }
    }
}
