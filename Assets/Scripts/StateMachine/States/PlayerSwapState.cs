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
        //locks cursor and enables the swap mode. 
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        player.StatsPlayer.enableSwapMode(); //mind capitals in the function name.
    }

    public override void ExitState()
    {
        //unlocks cursor when exiting the swap mode. 
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
