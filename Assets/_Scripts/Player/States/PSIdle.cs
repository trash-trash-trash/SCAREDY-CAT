using UnityEngine;

public class PSIdle : PlayerStateBase
{
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
    }

    private void ChangeAttackState(bool chargingAttack)
    {
        if(chargingAttack)
            playerBrain.ChangeState(PlayerStates.ChargingAttack);
    }

    private void ChangeJumpState(bool chargingJump)
    {
        if (chargingJump)
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