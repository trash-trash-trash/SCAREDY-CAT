using UnityEngine;

public class PSInMenu : PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerJump.FlipCanJump(false);
        playerBrain.rb.useGravity = false;
        playerBrain.playerMovement.readingUpDown = false;
        playerBrain.playerMovement.readingLeftRight = false;
    }
}
