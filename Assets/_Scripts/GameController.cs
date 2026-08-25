using UnityEngine;

public class GameController : MonoBehaviour
{
   public PlayerBrain playerBrain;

   public void StartGame()
   {
      playerBrain.ChangeState(PlayerStates.Idle);
   }
}
