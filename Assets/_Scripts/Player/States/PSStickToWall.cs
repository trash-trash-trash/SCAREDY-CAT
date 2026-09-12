using UnityEngine;

public class PSStickToWall : PlayerStateBase
{
    public float deadZone = 0f;

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
        //fall down if holding down
        if(playerBrain.playerMovement.moveInput.y < deadZone)
        {
            if (chargingJump)
            {
                FlipLookingForWallChecks(false);
                playerBrain.playerJump.CancelJump();
                playerBrain.ChangeState(PlayerStates.Falling);
            }
        }

        else if (chargingJump)
            playerBrain.ChangeState(PlayerStates.ChargingWallJump);
    }

    void Update()
    {
        // if(playerBrain.ledgeCheck.targetLayerDetected)
        //     playerBrain.ChangeState(PlayerStates.ClimbingUpLedge);

        if (playerBrain.groundCheck.targetLayerDetected)
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