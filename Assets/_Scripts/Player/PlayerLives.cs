using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public float catLives = 9;
    public float currentLives = 9;
    
    public void LoseALife()
    {
        currentLives--;
        if (currentLives <= 0)
        {
            //GameOver
        }
    }
}
