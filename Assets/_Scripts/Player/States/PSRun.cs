using UnityEngine;

public class PSRun : PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerAttack.FlipCanAttack(true);
        playerBrain.playerJump.FlipCanJump(true);
        playerBrain.rb.useGravity = true;
        playerBrain.playerMovement.readingUpDown = false;
        playerBrain.playerMovement.readingLeftRight = true;
        playerBrain.playerJump.AnnounceChargingJump += ChangeState;
        playerBrain.playerAttack.AnnounceChargingAttack += ChangeAttackState;
    }
    
    private void ChangeAttackState(bool chargingAttack)
    {
        if(chargingAttack)
            playerBrain.ChangeState(PlayerStates.ChargingAttack);
    }
    
    private void ChangeState(bool chargingJump)
    { 
        if(chargingJump)
            playerBrain.ChangeState(PlayerStates.ChargingJump);
    }

    void Update()
    {
        if(!playerBrain.groundCheck.targetLayerDetected)
            playerBrain.ChangeState(PlayerStates.Falling);
        
        if(playerBrain.playerMovement.moveInput == Vector2.zero)
            playerBrain.ChangeState(PlayerStates.Idle);
    }

    void OnDisable()
    {
        playerBrain.playerJump.AnnounceChargingJump -= ChangeState;
        playerBrain.playerAttack.AnnounceChargingAttack -= ChangeAttackState;
    }
}