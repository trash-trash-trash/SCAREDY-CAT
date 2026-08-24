using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] public LayerMask groundLayer;

    public bool isGrounded = false;
    
    private readonly Collider[] groundResults = new Collider[8];

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
    }

    private bool IsGrounded()
    {
        int count = Physics.OverlapSphereNonAlloc(
            groundCheck.position,
            groundCheckRadius,
            groundResults,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );

        return count > 0;
    }
}