using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(TestPlayerController player, PlayerStateFactory stateFactory) : base(player, stateFactory)
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
        float speed = player.StatsPlayer.WalkSpeed.Value;
        moveDirection.x = player.CurrentMovementInput.x * speed;
        moveDirection.z = player.CurrentMovementInput.y * speed;

        player.CurrentMoveDirection = moveDirection;
    }

    public override void CheckSwitchState()
    {
        if (player.IsRunPressed)
        {
            SwitchState(factory.Run());
        } else if (player.IsJumpPressed && player.IsGrounded)
        {
            SwitchState(factory.Jump());
        }
    }
}
