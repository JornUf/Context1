using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(TestPlayerController player, PlayerStateFactory stateFactory) : base(player, stateFactory)
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
        Vector3 moveDirection = player.CurrentMoveDirection;
        float speed = player.StatsPlayer.RunSpeed.Value;
        moveDirection.x = player.CurrentMovementInput.x * speed;
        moveDirection.z = player.CurrentMovementInput.y * speed;

        player.CurrentMoveDirection = moveDirection;
    }
    
    public override void CheckSwitchState()
    {
        if (!player.IsMovePressed)
        {
            SwitchState(factory.Idle());
        } else if (player.IsJumpPressed && player.IsGrounded)
        {
            SwitchState(factory.Jump());
        } else if (!player.IsRunPressed)
        {
            SwitchState(factory.Walk());
        }
    }
}
