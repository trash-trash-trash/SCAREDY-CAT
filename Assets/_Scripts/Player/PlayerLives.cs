using TMPro;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public TMP_Text lifeText;
    
    private float catLives = 9;
    public float currentLives = 9;
    
    public void LoseALife()
    {
        currentLives--;
        lifeText.text = currentLives.ToString();
    }

    public void Reset()
    {
        currentLives = catLives;
    }
}
