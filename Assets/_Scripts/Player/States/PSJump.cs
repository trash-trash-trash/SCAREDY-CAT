using System.Collections;
using UnityEngine;

public class PSJump : PlayerStateBase
{
    public bool leftGround = false;
    public float leaveGroundGraceTime = 0.3f;
    
    public override void OnEnable()
    {
        base.OnEnable();
        leftGround = false;
        StartCoroutine(LeaveGround());
    }

    IEnumerator LeaveGround()
    {
        yield return new WaitForSeconds(leaveGroundGraceTime);
        leftGround = true;
    }

    void Update()
    {
        if (!leftGround)
            return;
        
        // Player has reached the apex and is now falling
        if (playerBrain.rb.linearVelocity.y < 0f)
        {
            playerBrain.ChangeState(PlayerStates.Falling);
            return;
        }
        
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
