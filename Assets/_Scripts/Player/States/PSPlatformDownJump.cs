using System.Collections;
using UnityEngine;

public class PSPlatformDownJump : PlayerStateBase
{
    public bool leftGround = false;
    public float leaveGroundGraceTime = 0.3f;

    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerJump.CancelJump();
        leftGround = false;
        playerBrain.platformCollisionController.SetIgnoring(true);
        StartCoroutine(LeaveGround());
    }
    
    IEnumerator LeaveGround()
    {
        yield return new WaitForSeconds(leaveGroundGraceTime);
        leftGround = true;
        playerBrain.ChangeState(PlayerStates.Falling);
    }

    void OnDisable()
    {
        playerBrain.platformCollisionController.SetIgnoring(false);
    }
}
