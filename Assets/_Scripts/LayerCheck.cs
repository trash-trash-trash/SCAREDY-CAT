using System;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private Transform transformToCentreAround;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] public LayerMask layerToTarget;

    public bool targetLayerDetected = false;
    
    private readonly Collider[] groundResults = new Collider[8];

    private void FixedUpdate()
    {
        targetLayerDetected = TargetLayerDetected();
    }

    private bool TargetLayerDetected()
    {
        int count = Physics.OverlapSphereNonAlloc(
            transformToCentreAround.position,
            checkRadius,
            groundResults,
            layerToTarget,
            QueryTriggerInteraction.Ignore
        );

        return count > 0;
    }
}