using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SwapStatsPlayer _statsPlayer;
    [SerializeField] private Transform respawnPoint;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    
    [HideInInspector]
    public bool canMove = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? _statsPlayer.RunSpeed.Value : _statsPlayer.WalkSpeed.Value) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? _statsPlayer.RunSpeed.Value : _statsPlayer.WalkSpeed.Value) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = _statsPlayer.JumpSpeed.Value;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= _statsPlayer.GravitySpeed.Value * Time.deltaTime;
        }

        // Player and Camera rotation
        if (canMove)
        {    // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            StartSwapStuff();
        }

        if (!canMove && Input.GetKeyUp(KeyCode.E))
        {
            backtogame();
        }

        if (transform.position.y <= -5)
        {
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
            StartSwapStuff();
        }
    }

    public void backtogame()
    {
        canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void StartSwapStuff()
    {
        canMove = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _statsPlayer.enableSwapMode();
    }
}