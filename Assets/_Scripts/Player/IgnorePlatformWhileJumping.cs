using UnityEngine;

public class PlatformCollisionController : MonoBehaviour
{
    [SerializeField] private LayerMask layerToIgnore;

    public Rigidbody rb;

    private int objectLayer;

    // Automatically ignore platforms while travelling upward.
    public bool ignoreWhileRising;

    // Manual override.
    public bool manuallyIgnoring;

    public bool currentlyIgnoring;

    private void Awake()
    {
        objectLayer = gameObject.layer;
    }

    private void FixedUpdate()
    {
        // Ignore if either:
        // 1. We're manually ignoring platforms
        // 2. We're configured to ignore them while rising
        bool shouldIgnore =
            manuallyIgnoring ||
            (ignoreWhileRising && rb.linearVelocity.y > 0f);

        if (shouldIgnore == currentlyIgnoring)
            return;

        SetCollisionIgnoring(shouldIgnore);
    }

    public void SetIgnoring(bool input)
    {
        manuallyIgnoring = input;

        UpdateCollisionState();
    }

    public void SetIgnoreWhileRising(bool input)
    {
        ignoreWhileRising = input;

        UpdateCollisionState();
    }

    private void UpdateCollisionState()
    {
        bool shouldIgnore =
            manuallyIgnoring ||
            (ignoreWhileRising && rb.linearVelocity.y > 0f);

        if (shouldIgnore == currentlyIgnoring)
            return;

        SetCollisionIgnoring(shouldIgnore);
    }

    private void SetCollisionIgnoring(bool shouldIgnore)
    {
        currentlyIgnoring = shouldIgnore;

        for (int i = 0; i < 32; i++)
        {
            if ((layerToIgnore.value & (1 << i)) != 0)
            {
                Physics.IgnoreLayerCollision(
                    objectLayer,
                    i,
                    shouldIgnore
                );
            }
        }
    }
}