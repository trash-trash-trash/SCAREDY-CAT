using UnityEngine;

public class PSStickingToRoof : PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerJump.FlipCanJump(true);
        playerBrain.rb.useGravity = false;
        playerBrain.rb.linearVelocity = Vector3.zero;
        playerBrain.rb.angularVelocity = Vector3.zero;
        playerBrain.playerMovement.readingLeftRight = true;
        playerBrain.playerMovement.readingUpDown = false;   
        playerBrain.playerJump.AnnounceChargingJump += ChangeState;
    }

    private void ChangeState(bool chargingJump)
    {
        if(chargingJump)
            playerBrain.ChangeState(PlayerStates.ChargingRoofJump);
    }

    void Update()
    {
        if (!playerBrain.roofCheck.targetLayerDetected)
        {
            playerBrain.rb.useGravity = false;
            playerBrain.ChangeState(PlayerStates.Falling);
        }
    }

    void OnDisable()
    {
        playerBrain.playerJump.AnnounceChargingJump -= ChangeState;
    }
}