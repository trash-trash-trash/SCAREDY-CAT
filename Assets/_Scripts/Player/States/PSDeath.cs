using UnityEngine;

public class PSDeath : PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerLives.LoseALife();

        if (playerBrain.playerLives.currentLives <= 0)
        {
            //GameOver
        }
        else
        {
            //Teleport to last checkpoint
        }
    }
}
