using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformTrigger))]
public class Depth : MonoBehaviour
{
    private bool isRunning;

    private PlatformTrigger trigger; 
    [SerializeField] private PlatformVelocity velocity;
    
    private List<Vector3> points = new List<Vector3>();
    [SerializeField] private float offset = 10f; 
    [SerializeField] private float speed = 5;
    
    private Vector3 moveDirection;
    private int currentDirection = 1; //1 for upward movement, -1 for downward movement
    private Vector3 targetPoint = Vector3.zero;
    
    private void Awake()
    {
        Vector3 newpoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + offset);
        points.Add(newpoint);
        targetPoint = points[0];
        moveDirection = (targetPoint - transform.position).normalized;
        
        points.Add(transform.position);
        
        trigger = GetComponent<PlatformTrigger>();
        trigger.onTrigger.AddListener(MovePlatform);
    }

    private void Update()
    {
        SetDirection();
        
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
        if (currentDirection == 1)
        {
            if (trigger.runOnce)
            {
                isRunning = false;
                return; 
            }

            currentDirection *= -1;
            targetPoint = points[0];
        }
        else
        {
            currentDirection *= -1;
            targetPoint = points[1];
        }
    }

    private void MovePlatform()
    {
        FindNextPoint();
        isRunning = true; 
    }
    
    public void SetDirection()
    {
        if (!isRunning)
        {
            velocity.direction = Vector3.zero;
        }
        else
        {
            velocity.direction = moveDirection * currentDirection * speed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 newpoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + offset);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(newpoint, 0.2f);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(newpoint, transform.position);
    }
}
