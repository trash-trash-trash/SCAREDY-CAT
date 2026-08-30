using UnityEngine;

public class DSDefeated : DogStateBase
{
    public Sprite happydogSprite;
    
    public override void OnEnable()
    {
        base.OnEnable();
        dogBrain.spriteRenderer.sprite = happydogSprite;
        dogBrain.scalePulse.StartPulse();
        dogBrain.defeated = true;
        dogBrain.spriteRenderer.color = dogBrain.originalColor;
        dogBrain.investigateSpot.revealed = true;
    }
}
