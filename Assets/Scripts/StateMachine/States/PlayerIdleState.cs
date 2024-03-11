using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(TestPlayerController player, PlayerStateFactory stateFactory) : base(player, stateFactory)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
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
        if (player.IsMovePressed)
        {
            SwitchState(factory.Walk());
        } else if (player.IsJumpPressed && player.IsGrounded)
        {
            SwitchState(factory.Jump());
        }
    }
}
