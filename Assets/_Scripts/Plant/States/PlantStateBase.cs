using UnityEngine;

public class PlantStateBase : MonoBehaviour
{
    public PlantBrain plantBrain;
    
    public virtual void OnEnable()
    {
        plantBrain = GetComponentInParent<PlantBrain>();
    }
}