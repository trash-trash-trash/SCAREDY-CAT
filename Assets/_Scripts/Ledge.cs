using System;
using UnityEngine;

public class Ledge : MonoBehaviour
{
   public Transform associatedGroundPoint;

   private void OnTriggerEnter(Collider other)
   {
      PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

      if (playerBrain == null)
         return;

      if (playerBrain.currentState == PlayerStates.StickingToWall || playerBrain.currentState == PlayerStates.Jumping)
         playerBrain.StartLedgeClimb(associatedGroundPoint);
   }
}
