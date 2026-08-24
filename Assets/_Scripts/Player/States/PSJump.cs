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
