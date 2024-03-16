using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformTrigger : MonoBehaviour
{
    [HideInInspector] public UnityEvent onTrigger = new UnityEvent(); 
    
    private enum TriggerType
    {
        Static, 
        Dynamic, 
        StartAfterDelay,
        OnCollision
    }
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private float delay = 2f;
    public bool runOnce; // If true, the trigger will only run once

    private bool hasActived = false; 

    private void Start()
    {
        if (triggerType == TriggerType.Dynamic && onTrigger != null)
        {
            onTrigger.Invoke();
        }
    }

    private void Update()
    {
        if (triggerType == TriggerType.StartAfterDelay && onTrigger != null && !hasActived)
        {
            delay -= Time.deltaTime;
            if (delay <= 0f) 
            {
                onTrigger.Invoke();
                hasActived = true; 
            } 
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player" && triggerType == TriggerType.OnCollision && onTrigger != null && !hasActived) //maybe add collision layer? 
        {
            onTrigger.Invoke();
            hasActived = true; 
        }
    }
}
