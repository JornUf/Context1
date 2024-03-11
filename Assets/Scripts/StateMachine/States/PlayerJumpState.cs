using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(TestPlayerController player, PlayerStateFactory stateFactory) : base(player, stateFactory)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        //run jump here
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    
    public override void CheckSwitchState()
    {
        if (player.IsGrounded)
        {
            SwitchState(factory.Idle());
        } 
    }
}
