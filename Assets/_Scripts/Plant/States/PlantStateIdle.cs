using UnityEngine;

public class PlantStateIdle : PlantStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        plantBrain.investigateSpot.revealed = false;
    }
}