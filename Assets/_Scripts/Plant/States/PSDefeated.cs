using UnityEngine;

public class PSDefeated : PlantStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        plantBrain.health.FlipCanTakeDamage(false);
        plantBrain.investigateSpot.revealed = true;
    }
}
