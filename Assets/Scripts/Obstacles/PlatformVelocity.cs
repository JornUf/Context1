using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformVelocity : MonoBehaviour
{
    public UnityEvent onCollisionTrigger;
    private bool hasTriggered = false; 
    
    public Vector3 direction = Vector3.zero; 

    private bool isOnPlatform = false; 

    private PlayerController player;
    
    void Update()
    {
        if (isOnPlatform)
        {
            player.ExternalMoveDirection = direction;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("found player");
            if (!player)
            {
                player = other.gameObject.GetComponent<PlayerController>();
            }

            if (!hasTriggered)
            {
                onCollisionTrigger.Invoke();
                hasTriggered = true; 
            }

            isOnPlatform = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player left");
            isOnPlatform = false; 
            player.ExternalMoveDirection = Vector3.zero;
        }
    }
}
