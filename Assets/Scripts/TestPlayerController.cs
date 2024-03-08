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

    private CharacterController characterController;
    private PlayerInput playerInput;

    private Vector2 currentMovementInput;
    private Vector3 currentMoveDirection;

    private bool isGrounded; 
    private bool isMovePressed;
    private bool isRunPressed;
    private bool isJumpPressed;
    private bool isSwapPressed; 
    
    
    //statemachine stuff 
    private PlayerState currentState;
    private PlayerStateFactory states;
    
    //getters + setters 
    public PlayerState CurrentState
    {
        get { return currentState;}
        set { currentState = value; }
    }
    
    public SwapStatsPlayer StatsPlayer { get { return statsPlayer; } }
    public bool IsGrounded { get { return isGrounded; } }
    public bool IsJumpPressed {get { return isJumpPressed; } }
    
    

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
    }

    public void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMoveDirection.x = currentMovementInput.x;
        currentMoveDirection.z = currentMovementInput.y;
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

    private void Update()
    {
        //updates active state
        currentState.UpdateState();
        currentState.CheckSwitchState();
    }
}
