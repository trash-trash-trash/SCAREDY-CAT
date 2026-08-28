using UnityEngine;

public class PSDefeated : PlantStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        plantBrain.investigateSpot.revealed = true;
    }
}
