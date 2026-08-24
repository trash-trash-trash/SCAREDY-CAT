using UnityEngine;

public class PSFalling: PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerJump.FlipCanJump(false);
        playerBrain.playerMovement.readingUpDown = false;
        playerBrain.playerMovement.readingLeftRight = false;
    }

    void Update()
    {
        if(playerBrain.leftWallCheck.targetLayerDetected )
        {
            playerBrain.currentWallCheck  = playerBrain.leftWallCheck;
            playerBrain.ChangeState(PlayerStates.StickingToWall);
        }
        else if (playerBrain.rightWallCheck.targetLayerDetected)
        {
            playerBrain.currentWallCheck  = playerBrain.rightWallCheck;
            playerBrain.ChangeState(PlayerStates.StickingToWall);
        }
        else if (playerBrain.groundCheck.targetLayerDetected)
            playerBrain.ChangeState(PlayerStates.Idle);
    }
}