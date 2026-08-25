using UnityEngine;

public class PSClimbingUpLedge : PlayerStateBase
{
    [SerializeField] private float climbDuration = 0.5f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timer;
    
    

    public override void OnEnable()
    {
        base.OnEnable();
        
        playerBrain.playerJump.FlipCanJump(false);
        
        timer = 0f;

        startPosition = playerBrain.rb.position;
        targetPosition = playerBrain.ledgeTarget.position;
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        float t = Mathf.Clamp01(timer / climbDuration);

        Vector3 newPosition = Vector3.Lerp(
            startPosition,
            targetPosition,
            t
        );

        playerBrain.rb.MovePosition(newPosition);

        if (t >= 1f)
        {
            playerBrain.rb.position = targetPosition;
            playerBrain.ChangeState(PlayerStates.Idle);
        }
    }
}