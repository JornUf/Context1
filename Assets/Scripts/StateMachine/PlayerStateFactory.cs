using System.Collections.Generic;

public class PlayerStateFactory
{
    private TestPlayerController player;
    Dictionary<string, PlayerState> states = new Dictionary<string, PlayerState>();
    
    public PlayerStateFactory(TestPlayerController player)
    {
        this.player = player;
        states["idle"] = new PlayerIdleState(player, this);
        states["walk"] = new PlayerWalkState(player, this);
        states["run"] = new PlayerRunState(player, this);
        states["jump"] = new PlayerJumpState(player, this);
        states["swap"] = new PlayerSwapState(player, this);
    }
    public PlayerState Idle()
    {
        return states["idle"];
    }
    public PlayerState Walk()
    {
        return states["walk"];
    }
    public PlayerState Run()
    {
        return states["run"];
    }
    public PlayerState Jump()
    {
        return states["jump"];
    }
    public PlayerState Swap()
    {
        return states["swap"];
    }
}
