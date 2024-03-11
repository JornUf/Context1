using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwapState : PlayerState
{
    public PlayerSwapState(TestPlayerController player, PlayerStateFactory stateFactory) : base(player, stateFactory)
    {
    }

    public override void EnterState()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        player.StatsPlayer.enableSwapMode();
    }

    public override void ExitState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    
    public override void CheckSwitchState()
    {
        if (!player.IsSwapPressed && player.IsGrounded)
        {
            SwitchState(factory.Idle());
        }
    }
}
