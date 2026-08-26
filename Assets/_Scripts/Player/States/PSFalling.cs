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
        playerBrain.playerMovement.readingLeftRight = false;
    }

    void Update()
    {
        if(playerBrain.leftWallCheck.targetLayerDetected )
        {
            playerBrain.leftWall = true;
            playerBrain.currentWallCheck  = playerBrain.leftWallCheck;
            playerBrain.ChangeState(PlayerStates.StickingToWall);
        }
        else if (playerBrain.rightWallCheck.targetLayerDetected)
        {
            playerBrain.leftWall = false;
            playerBrain.currentWallCheck  = playerBrain.rightWallCheck;
            playerBrain.ChangeState(PlayerStates.StickingToWall);
        }
        else if (playerBrain.groundCheck.targetLayerDetected)
            playerBrain.ChangeState(PlayerStates.Idle);
    }
}