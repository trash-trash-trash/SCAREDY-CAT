using UnityEngine;

public class PSIdle : PlayerStateBase
{
    //assume 0, make -0.5f etc for controller deadzones
    public float deadZone = 0;

    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerAttack.FlipCanAttack(true);
        playerBrain.playerJump.FlipCanJump(true);
        playerBrain.rb.useGravity = true;
        playerBrain.playerMovement.readingUpDown = false;
        playerBrain.playerMovement.readingLeftRight = true;
        playerBrain.playerJump.AnnounceChargingJump += ChangeJumpState;
        playerBrain.playerAttack.AnnounceChargingAttack += ChangeAttackState;
        playerBrain.rb.isKinematic = false;
        playerBrain.health.FlipCanTakeDamage(true);
    }

    private void ChangeAttackState(bool chargingAttack)
    {
        if (chargingAttack)
            playerBrain.ChangeState(PlayerStates.ChargingAttack);
    }

    private void ChangeJumpState(bool chargingJump)
    {
        //if you're on a platform
        if (playerBrain.platformCheck.targetLayerDetected)
        {
            //and you're holding down
            //jump down thru a platform
            if (chargingJump)
                if (playerBrain.playerMovement.moveInput.y < deadZone)
                    playerBrain.ChangeState(PlayerStates.PlatformDownJump);
                else
                    playerBrain.ChangeState(PlayerStates.ChargingJump);
        }

        //else charge jump
        else if (chargingJump)
            playerBrain.ChangeState(PlayerStates.ChargingJump);
    }

    void Update()
    {
        if (!playerBrain.groundCheck.targetLayerDetected)
            playerBrain.ChangeState(PlayerStates.Falling);

        if (Mathf.Abs(playerBrain.playerMovement.moveInput.x) > 0.01f)
            playerBrain.ChangeState(PlayerStates.Walking);
    }

    void OnDisable()
    {
        playerBrain.playerAttack.AnnounceChargingAttack -= ChangeAttackState;
        playerBrain.playerJump.AnnounceChargingJump -= ChangeJumpState;
    }
}