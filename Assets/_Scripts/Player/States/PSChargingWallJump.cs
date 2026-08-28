using UnityEngine;

public class PSChargingWallJump : PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        
        playerBrain.rb.angularVelocity = Vector3.zero;
        playerBrain.rb.linearVelocity = Vector3.zero;
        playerBrain.playerMovement.readingUpDown = false;
        playerBrain.playerJump.AnnounceChargingJump += ChangeState;
    }

    private void ChangeState(bool chargingJump)
    {
        if(!chargingJump)
        {    
            if (playerBrain.playerMovement.moveInput.x != 0)
               playerBrain.HardFlip();
    
            playerBrain.ChangeState(PlayerStates.Jumping);
        }
    }

    void OnDisable()
    {
        playerBrain.playerJump.AnnounceChargingJump -= ChangeState;
        playerBrain.rb.useGravity = true;
    }
}
