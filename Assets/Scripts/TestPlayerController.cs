using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class TestPlayerController : MonoBehaviour
{
    
    [SerializeField] private SwapStatsPlayer statsPlayer;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Camera playerCamera;

    private CharacterController characterController;
    private PlayerInput playerInput;

    private Vector2 currentMovementInput;
    private Vector3 currentMoveDirection = Vector3.zero;
    
    //camera rotation vars
    private float rotationX = 0;
    [Header("Camera Rotation")]
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;
    
    
    //input states
    private bool isMovePressed;
    private bool isRunPressed;
    private bool isJumpPressed;
    private bool isSwapPressed; 
    
    
    //statemachine stuff (want to protect my code for some reason)
    private PlayerState currentState;
    private PlayerStateFactory states;
    
    //getters + setters 
    public PlayerState CurrentState { get { return currentState;} set { currentState = value; } }
    public CharacterController CharacterController { get { return characterController; } }
    public SwapStatsPlayer StatsPlayer { get { return statsPlayer; } }
    public Vector2 CurrentMovementInput { get { return currentMovementInput; } }
    public Vector3 CurrentMoveDirection { get { return currentMoveDirection; } set { currentMoveDirection = value; } }
    public bool IsGrounded { get { return characterController.isGrounded; } }
    public bool IsMovePressed { get { return isMovePressed; } }
    public bool IsRunPressed { get { return isRunPressed; } }
    public bool IsJumpPressed {get { return isJumpPressed; } }
    public bool IsSwapPressed { get { return isSwapPressed; } }

    public void Awake()
    {
        //intitializes statemachine and states 
        states = new PlayerStateFactory(this);
        currentState = states.Idle();
        currentState.EnterState();
        
        //intializes input system (version 1.01)
        playerInput = new PlayerInput();
        
        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        
        playerInput.CharacterControls.Sprint.started += OnSprintInput;
        playerInput.CharacterControls.Sprint.canceled += OnSprintInput;
        
        playerInput.CharacterControls.Jump.started += OnJumpInput;
        playerInput.CharacterControls.Jump.canceled += OnJumpInput;
        
        playerInput.CharacterControls.EnterSwapMode.started += OnSwapInput;
        playerInput.CharacterControls.EnterSwapMode.canceled += OnSwapInput;
    }

    public void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        Debug.Log(currentMovementInput);
        isMovePressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }
    
    private void OnSprintInput(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();   
    }

    private void OnSwapInput(InputAction.CallbackContext context)
    {
        //if swap is pressed, state machine will switch to swap state.
        if (!isSwapPressed)
        {
            isSwapPressed = context.ReadValueAsButton();
            currentState.OverrideState(states.Swap());
        }
    }

    private void Update()
    {
        HandleRotation();
        
        //updates active state
        currentState.CheckSwitchState();
        currentState.UpdateState();

        characterController.Move(currentMoveDirection * Time.deltaTime);
        
        HandleGravity();
    }
    
    private void HandleRotation()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void HandleGravity()
    {
        if (!characterController.isGrounded)
        {
            currentMoveDirection.y -= statsPlayer.GravitySpeed.Value;
        }
    }

    public void BackToGame() //function that may be changed if Jorn implements swapping differenty
    {
        isSwapPressed = false;
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
