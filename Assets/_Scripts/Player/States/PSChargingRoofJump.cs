using UnityEngine;

public class PSChargingRoofJump : PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerJump.FlipInvertedJump(true);
        playerBrain.rb.angularVelocity = Vector3.zero;
        playerBrain.rb.linearVelocity = Vector3.zero;
        playerBrain.playerMovement.readingLeftRight = false;
        playerBrain.playerMovement.readingUpDown = false;
        playerBrain.playerJump.AnnounceChargingJump += ChangeState;
    }

    private void ChangeState(bool chargingJump)
    {
        if(!chargingJump)
        {    
        //     if (playerBrain.playerMovement.moveInput.x != 0)
        //         playerBrain.HardFlip();
    
            playerBrain.FlipSprite180();
            playerBrain.ChangeState(PlayerStates.Falling);
        }
    }

    void OnDisable()
    {
        playerBrain.playerJump.AnnounceChargingJump -= ChangeState;
        playerBrain.rb.useGravity = true;
        playerBrain.playerJump.FlipInvertedJump(false);
    }
}
