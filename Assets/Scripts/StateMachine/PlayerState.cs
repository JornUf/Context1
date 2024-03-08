using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//template for every non-parent state.
public abstract class PlayerState
{

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

    protected void SwitchState(PlayerState newstate)
    {
        //switches between states ... duh
        ExitState();
        newstate.EnterState();
        
        player.CurrentState = newstate;
    }
}
