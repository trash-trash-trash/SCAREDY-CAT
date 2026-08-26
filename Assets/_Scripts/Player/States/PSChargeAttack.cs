using UnityEngine;

public class PSChargeAttack : PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerJump.FlipCanJump(false);
        playerBrain.rb.angularVelocity = Vector3.zero;
        playerBrain.rb.linearVelocity = Vector3.zero;
        playerBrain.playerMovement.readingLeftRight = false;
        playerBrain.playerAttack.AnnounceChargingAttack += ChangeState;
    }

    private void ChangeState(bool chargingAttack)
    {
        if(!chargingAttack)
            playerBrain.ChangeState(PlayerStates.Attacking);
    }
 
    void OnDisable()
    {
        playerBrain.playerAttack.AnnounceChargingAttack -= ChangeState;
    }
}
