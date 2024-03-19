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
        player.Animator.SetBool("Walk", true);
    }

    public override void ExitState()
    {
        base.ExitState();
        player.Animator.SetBool("Walk", false);
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
        if (!player.IsMovePressed)
        {
            SwitchState(factory.Idle());
        } else if (player.IsJumpPressed && player.IsGrounded)
        {
            SwitchState(factory.Jump());
        } else if (player.IsRunPressed)
        {
            SwitchState(factory.Run());
        }
    }
}
