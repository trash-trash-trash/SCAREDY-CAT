using UnityEngine;

public class PSStickToWall : PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerAttack.FlipCanAttack(false);
        playerBrain.playerJump.FlipCanJump(true);
        playerBrain.playerMovement.readingLeftRight = false;
        playerBrain.playerMovement.readingUpDown = true;
        playerBrain.rb.useGravity = false;
        playerBrain.playerJump.AnnounceChargingJump += ChangeState;
    }

    private void ChangeState(bool chargingJump)
    {
        if(chargingJump)
            playerBrain.ChangeState(PlayerStates.ChargingWallJump);
    }

    void Update()
    {
        // if(playerBrain.ledgeCheck.targetLayerDetected)
        //     playerBrain.ChangeState(PlayerStates.ClimbingUpLedge);
        
        if(playerBrain.groundCheck.targetLayerDetected)
            playerBrain.ChangeState(PlayerStates.Idle);
        
        else if (!playerBrain.currentWallCheck.targetLayerDetected && !playerBrain.groundCheck.targetLayerDetected)
        {
            playerBrain.currentWallCheck = null;
            playerBrain.ChangeState(PlayerStates.Falling);
        }       
    }

    void OnDisable()
    {
        playerBrain.playerJump.AnnounceChargingJump -= ChangeState;
    }
}
