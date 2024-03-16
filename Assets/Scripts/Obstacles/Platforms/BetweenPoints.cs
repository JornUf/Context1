using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformTrigger))]
public class BetweenPoints : MonoBehaviour
{
    private bool isRunning;

    private PlatformTrigger trigger; 
    
    [SerializeField] private List<Vector3> points = new List<Vector3>();
    [SerializeField] private Transform basicTargetPoint; 
    [SerializeField] private float speed = 1;

    private int currentIndex = 0;
    private int currentDirection = 1; //1 for upward movement, -1 for downward movement
    private Vector3 targetPoint = Vector3.zero;
    
    private void Awake()
    {
        if (points.Count < 2)
        {
            points.Add(transform.position);
            
            if (basicTargetPoint != null)
                points.Add(basicTargetPoint.position);
            else
                points.Add(transform.position + Vector3.up * 2);
            
            Debug.Log("platform " + gameObject.name + " has less than 2 points, adding default points");
        }
        
        trigger = GetComponent<PlatformTrigger>();
        trigger.onTrigger.AddListener(MovePlatform);
    }

    private void Update()
    {
        if (isRunning)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            
            float distance = (targetPoint - transform.position).magnitude;
            if (distance <= 0.1f)
            {
                FindNextPoint();
            }
        }
    }

    private void FindNextPoint()
    {
        targetPoint = points[currentIndex];
        
        switch (currentDirection)
        {
            case 1:
                if (currentIndex == points.Count - 1)
                {
                    currentDirection *= -1;
                } else
                {
                    currentIndex += currentDirection;
                }
                break;
            case -1:
                if (currentIndex == 0)
                {
                    currentDirection *= -1;
                } else
                {
                    if (trigger.runOnce) //stops platform when it reaches the end of the path
                    {
                        isRunning = false;
                        return; 
                    }
                    currentIndex += currentDirection;
                }
                break;
        }
    }

    private void MovePlatform()
    {
        FindNextPoint();
        isRunning = true; 
    }

    private void OnDrawGizmosSelected()
    {
        if (points.Count >= 2) 
        {
            Vector3 lastPoint = points[0];
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(points[0], 0.2f);
            for (int i = 1; i < points.Count; i++)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(points[i], 0.2f);
                
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(lastPoint, points[i]);
                lastPoint = points[i];
            }
        }
    }
}
