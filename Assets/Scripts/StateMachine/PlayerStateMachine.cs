using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{

    //current state can be only be read from different classes. Calling "ChangeState()" is required to go to a different state.
    public PlayerState currentState { get; private set;}
    
    public void StartStateMachine(PlayerState startState)
    {
        currentState = startState;
        startState.EnterState();
    }

    public void SwitchState(PlayerState newState)
    {
        currentState.ExitState();
        currentState = newState;
        newState.EnterState();
    }
}
