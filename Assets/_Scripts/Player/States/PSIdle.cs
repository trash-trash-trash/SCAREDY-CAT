using UnityEngine;

public class PSIdle : PlayerStateBase
{
   public override void OnEnable()
   {
      base.OnEnable();
      playerBrain.playerJump.FlipCanJump(true);
      playerBrain.rb.useGravity = true;
      playerBrain.playerMovement.readingUpDown = false;
      playerBrain.playerMovement.readingLeftRight = true;
      playerBrain.playerJump.AnnounceChargingJump += ChangeState;
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
   }

   void OnDisable()
   {
      playerBrain.playerJump.AnnounceChargingJump -= ChangeState;
   }
}
