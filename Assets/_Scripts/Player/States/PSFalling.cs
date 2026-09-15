using UnityEngine;

public class PSFalling: PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerAttack.FlipCanAttack(false);
        playerBrain.rb.useGravity = true;
        playerBrain.playerJump.FlipCanJump(false);
        playerBrain.playerMovement.readingUpDown = false;
        
        if(!playerBrain.airControl)
            playerBrain.playerMovement.readingLeftRight = false;
    }

    void Update()
    {
        //0 / deadzone for controllers
        //holding down while falling lets you fall thru platforms
        if(playerBrain.playerMovement.moveInput.y < 0)
            playerBrain.platformCollisionController.SetIgnoring(true);
        else
            playerBrain.platformCollisionController.SetIgnoring(false);
        
        if(playerBrain.lookingForWallChecks)
        {
            if (playerBrain.leftWallCheck.targetLayerDetected)
            {
                playerBrain.leftWall = true;
                playerBrain.currentWallCheck = playerBrain.leftWallCheck;
                playerBrain.ChangeState(PlayerStates.StickingToWall);
            }
            else if (playerBrain.rightWallCheck.targetLayerDetected)
            {
                playerBrain.leftWall = false;
                playerBrain.currentWallCheck = playerBrain.rightWallCheck;
                playerBrain.ChangeState(PlayerStates.StickingToWall);
            }
        }
        
        if (playerBrain.groundCheck.targetLayerDetected)
            playerBrain.ChangeState(PlayerStates.Idle);
    }

    void OnDisable()
    {
        playerBrain.platformCollisionController.SetIgnoring(false);
    }
}