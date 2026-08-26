using UnityEngine;

public class PSUnhide : PlayerStateBase
{
    [SerializeField] private float hideDuration = 0.3f;
    [SerializeField] private float hopHeight = 0.2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timer;

    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerAttack.FlipCanAttack(false);
        playerBrain.rb.useGravity = false;
        playerBrain.hiding = true;
        playerBrain.playerJump.FlipCanJump(false);
        playerBrain.playerMovement.readingLeftRight = false;

        timer = 0f;

        startPosition = playerBrain.rb.position;
        targetPosition = playerBrain.positionBeforeHiding;
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        float t = Mathf.Clamp01(timer / hideDuration);

        Vector3 newPosition = Vector3.Lerp(
            startPosition,
            targetPosition,
            t
        );

        // Small hop during the movement
        float hop = Mathf.Sin(t * Mathf.PI) * hopHeight;
        newPosition.y += hop;

        playerBrain.rb.MovePosition(newPosition);

        if (t >= 1f)
        {
            playerBrain.rb.position = targetPosition;
            playerBrain.ChangeState(PlayerStates.Idle);
        }
    }

    private void OnDisable()
    {
        playerBrain.rb.useGravity = true;
        playerBrain.hiding = false;
    }
}