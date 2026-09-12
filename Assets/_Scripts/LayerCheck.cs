using System;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private Transform transformToCentreAround;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] public LayerMask layerToTarget;

    public bool targetLayerDetected = false;

    private readonly Collider[] groundResults = new Collider[8];

    public bool lookingForTarget = true;

    public void FlipLookingForTarget(bool input)
    {
        lookingForTarget = input;
    }

    private void FixedUpdate()
    {
        if (!lookingForTarget)
            return;

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