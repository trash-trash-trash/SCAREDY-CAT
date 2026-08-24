using UnityEngine;

public class PSChargeJump : PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerMovement.readingLeftRight = false;
        playerBrain.playerJump.AnnounceChargingJump += ChangeState;
    }

    private void ChangeState(bool chargingJump)
    {
        if(!chargingJump)
            playerBrain.ChangeState(PlayerStates.Jumping);
    }

    void OnDisable()
    {
        playerBrain.playerJump.FlipCanJump(false);
        playerBrain.playerJump.AnnounceChargingJump -= ChangeState;
    }
}
