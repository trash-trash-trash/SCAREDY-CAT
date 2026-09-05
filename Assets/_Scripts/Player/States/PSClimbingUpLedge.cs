using UnityEngine;

public class PSClimbingUpLedge : PlayerStateBase
{
    [SerializeField] private float moveToCenterDuration = 0.2f;
    [SerializeField] private float climbPointDuration = 0.15f;

    private Ledge ledge;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private float timer;
    private int currentPointIndex;
    private bool movingToCenter;

    public override void OnEnable()
    {
        base.OnEnable();

        playerBrain.rb.linearVelocity = Vector3.zero;
        playerBrain.rb.angularVelocity = Vector3.zero;
        
        ledge = playerBrain.currentLedge;
        
        playerBrain.playerJump.FlipCanJump(false);

        timer = 0f;
        currentPointIndex = 0;
        movingToCenter = true;

        startPosition = playerBrain.rb.position;

        // Center of the ledge hitbox
        targetPosition = ledge.GetComponent<Collider>().bounds.center;
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        float duration = movingToCenter
            ? moveToCenterDuration
            : climbPointDuration;

        float t = Mathf.Clamp01(timer / duration);

        Vector3 newPosition = Vector3.Lerp(
            startPosition,
            targetPosition,
            t
        );

        playerBrain.rb.MovePosition(newPosition);

        if (t < 1f)
            return;

        playerBrain.rb.MovePosition(targetPosition);

        if (movingToCenter)
        {
            // We've reached the center of the ledge.
            movingToCenter = false;
            currentPointIndex = 0;

            MoveToNextClimbPoint();
        }
        else
        {
            currentPointIndex++;

            if (currentPointIndex >= ledge.climbPoints.Count)
            {
                // Finished climbing.
                playerBrain.rb.MovePosition(
                    ledge.associatedGroundPoint.position
                );

                playerBrain.ChangeState(PlayerStates.Idle);
                return;
            }

            MoveToNextClimbPoint();
        }
    }

    private void MoveToNextClimbPoint()
    {
        timer = 0f;
        startPosition = playerBrain.rb.position;
        targetPosition = ledge.climbPoints[currentPointIndex].position;
    }

    void OnDisable()
    {
        playerBrain.currentLedge = null;
    }
}
