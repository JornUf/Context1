using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//template for every non-parent state.
public abstract class PlayerState
{
    //playercontroller and statefactory
    protected TestPlayerController player;
    protected PlayerStateFactory factory; 
    
    public PlayerState(TestPlayerController playerConroller, PlayerStateFactory stateFactory)
    {
        player = playerConroller;
        factory = stateFactory;
    }
    
    public virtual void EnterState() {} 
    public virtual void ExitState() {}
    public virtual void UpdateState() {}
    public virtual void CheckSwitchState() {}

    protected void SwitchState(PlayerState newState)
    {
        //switches between states ... duh
        ExitState();
        newState.EnterState();

        player.CurrentState = newState;
    }
    public void OverrideState(PlayerState newState)
    {
        //only call this function when a direct override of the current state is required. 
        //in this context for entering the swap state only!
        SwitchState(newState);
    }
}
