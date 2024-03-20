using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(TestPlayerController player, PlayerStateFactory stateFactory) : base(player, stateFactory)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.Animator.SetBool("Moving", true);
    }

    public override void ExitState()
    {
        base.ExitState();
        player.Animator.SetBool("Moving", false);
    }

    public override void UpdateState()
    {
        Vector3 moveDirection = player.CurrentMoveDirection;

        float speed = player.StatsPlayer.WalkSpeed.Value;
        moveDirection.x = player.CurrentMovementInput.x * speed;
        moveDirection.z = player.CurrentMovementInput.y * speed;

        player.Animator.SetFloat("X Velocity", Mathf.Lerp(player.Animator.GetFloat("X Velocity"), player.CurrentMovementInput.x, 8 * Time.deltaTime));
        player.Animator.SetFloat("Z Velocity", Mathf.Lerp(player.Animator.GetFloat("Z Velocity"), player.CurrentMovementInput.y, 8 * Time.deltaTime));
        player.Animator.SetFloat("Magnitude", moveDirection.magnitude);

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
