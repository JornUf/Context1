using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private Vector3 moveDirection;
    private int jumpCount; 
    public PlayerJumpState(TestPlayerController player, PlayerStateFactory stateFactory) : base(player, stateFactory)
    {
    }

    public override void EnterState()
    {
        jumpCount = 0; 
        moveDirection = player.CurrentMoveDirection;
        
        Jump(player.StatsPlayer.JumpSpeed.Value);
        player.Animator.SetBool("Jumping", true);
    }

    public override void ExitState()
    {
        player.Animator.SetBool("Jumping", false);
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Vector3 moveDirection = player.CurrentMoveDirection;
        float speed = player.StatsPlayer.WalkSpeed.Value * 0.8f;
        moveDirection.x = player.CurrentMovementInput.x * speed;
        moveDirection.z = player.CurrentMovementInput.y * speed;

        player.CurrentMoveDirection = moveDirection;
    }
    
    public override void CheckSwitchState()
    {
        if (player.IsGrounded)
        {
            player.Animator.SetTrigger("Landing");
            SwitchState(factory.Idle());
        } 
    }

    private void Jump(float jumpForce)
    {
        //consider looking at this for the jump https://www.youtube.com/watch?v=hG9SzQxaCm8. This will make it feel much more realistic and less snappy. 
        moveDirection.y = jumpForce; 
        player.CurrentMoveDirection = moveDirection;
    }
}
