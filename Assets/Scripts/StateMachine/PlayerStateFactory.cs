public class PlayerStateFactory
{
    private TestPlayerController player;

    public PlayerStateFactory(TestPlayerController player)
    {
        this.player = player;
    }
    public PlayerState Idle()
    {
        return new PlayerIdleState(player, this);
    }
    public PlayerState Walk()
    {
        return new PlayerWalkState(player, this);
    }
    public PlayerState Run()
    {
        return new PlayerRunState(player, this); 
    }
    public PlayerState Jump()
    {
        return new PlayerJumpState(player, this);
    }
    public PlayerState Swap()
    {
        return new PlayerSwapState(player, this);
    }
}
