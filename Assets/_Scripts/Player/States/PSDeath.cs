using UnityEngine;

public class PSDeath : PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerJump.FlipCanJump(false);
        playerBrain.playerAttack.FlipCanAttack(false);
        playerBrain.playerMovement.readingLeftRight = false;
        playerBrain.playerMovement.readingUpDown = false;
    }
}
