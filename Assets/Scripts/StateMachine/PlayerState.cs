using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//template for every non-parent state.
public abstract class PlayerState
{

    protected TestPlayerController player;
    protected PlayerStateMachine stateMachine;
    
    public virtual void EnterState() {} 
    public virtual void ExitState() {}
    public virtual void UpdateState() {}

    public PlayerState(TestPlayerController player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }
}
