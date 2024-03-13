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
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        //add double jump logic here
    }
    
    public override void CheckSwitchState()
    {
        if (player.IsGrounded)
        {
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
