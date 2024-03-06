using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TestPlayerController : MonoBehaviour
{
    
    [SerializeField] private SwapStatsPlayer statsPlayer;
    [SerializeField] private Transform respawnPoint;

    private CharacterController characterController;
    private PlayerInput playerInput;

    private Vector2 currentMovementInput;
    private Vector3 currentMoveDirection; 
    
    //statemachine stuff 
    public PlayerStateMachine StateMachine;

    public PlayerIdleState IdleState;
    public PlayerWalkState WalkState;
    public PlayerRunState RunState;
    public PlayerJumpState JumpState;
    public PlayerSwapState SwapState;

    public void Awake()
    {
        //intitializes statemachine and states 
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine);
        WalkState = new PlayerWalkState(this, StateMachine);
        RunState = new PlayerRunState(this, StateMachine);
        JumpState = new PlayerJumpState(this, StateMachine);
        SwapState = new PlayerSwapState(this, StateMachine);
        
        //intializes input system (version 1.01)
        playerInput = new PlayerInput();
    }
}
