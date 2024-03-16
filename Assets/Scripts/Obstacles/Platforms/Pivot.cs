using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformTrigger))]
public class Pivot : MonoBehaviour
{
    private bool isRunning = false; 
    
    private PlatformTrigger trigger;

    [SerializeField] private Vector3 pivotDisplacement;
    [SerializeField] private float rotationSpeed = 50f;
    
    private Vector3 pivotPoint;
    private Vector3 startPoint; 

    private void Awake()
    {
        pivotPoint = transform.position + pivotDisplacement;
        startPoint = pivotPoint;
        
        trigger = GetComponent<PlatformTrigger>();
        trigger.onTrigger.AddListener(StartRunning);
    }
    private void Update()
    {
        if (isRunning)
        {
            transform.RotateAround(pivotPoint, Vector3.up, rotationSpeed * Time.deltaTime);
            if (pivotPoint == startPoint && trigger.runOnce)
            {
                isRunning = false;
            }
        }
    }
    private void StartRunning()
    {
        isRunning = true;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 i = transform.position + pivotDisplacement;
        
        Gizmos.color = Color.blue; 
        Gizmos.DrawWireSphere(i, 0.2f);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(i, Vector3.Distance(transform.position, i));
    }
}
