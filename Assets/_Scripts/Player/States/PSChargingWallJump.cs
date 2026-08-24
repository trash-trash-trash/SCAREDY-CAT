using UnityEngine;

public class PSChargingWallJump : PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerMovement.readingUpDown = false;
        playerBrain.playerJump.AnnounceChargingJump += ChangeState;
    }

    private void ChangeState(bool chargingJump)
    {
        if(!chargingJump)
            playerBrain.ChangeState(PlayerStates.Jumping);
    }

    void OnDisable()
    {
        playerBrain.rb.useGravity = true;
    }
}
