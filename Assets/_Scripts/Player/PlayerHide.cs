using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHide : MonoBehaviour
{
   public PlayerBrain playerBrain;
   public PlayerInputs playerInputs;

   void Awake()
   {
      playerInputs.AnnounceInteractAction += TryHide;
   }

   private void TryHide(InputAction.CallbackContext obj)
   {
      if (!playerBrain.canHide)
         return;
      
      if (obj.performed)
      {
         if(!playerBrain.hiding)
         {
            playerBrain.ChangeState(PlayerStates.Hiding);
            playerBrain.currentHidingSpot.Hide();
         }
         else
         {
            playerBrain.ChangeState(PlayerStates.Idle);
         }
      }
   }

   void OnDestroy()
   {
      playerInputs.AnnounceInteractAction -= TryHide;
   }
}
