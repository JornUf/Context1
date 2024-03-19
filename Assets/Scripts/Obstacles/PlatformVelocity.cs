using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVelocity : MonoBehaviour
{
    public Vector3 direction = Vector3.zero; 

    private bool isOnPlatform = false; 

    private TestPlayerController player;
    
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
            if (!player)
            {
                player = other.gameObject.GetComponent<TestPlayerController>();
            }

            isOnPlatform = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOnPlatform = false; 
            player.ExternalMoveDirection = Vector3.zero;
        }
    }
}
