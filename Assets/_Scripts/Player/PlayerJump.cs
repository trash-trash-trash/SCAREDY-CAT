using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings You Can Fiddle With")]
    [SerializeField] private float minJumpPower = 5f;
    [SerializeField] private float maxJumpPower = 15f;
    
    [SerializeField] private float minHorizontalPower = 2f;
    [SerializeField] private float maxHorizontalPower = 10f;
    
    [SerializeField] private float chargeTime = 1f;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInputs inputHandler;

    public GroundCheck groundCheck;

    private Vector2 moveInput;

    public bool chargingJump;
    public float chargeTimer;
    public float jumpPower;
    
    private void OnEnable()
    {
        inputHandler.AnnounceJumpAction += HandleJump;
    }

    private void Update()
    {
        moveInput = inputHandler.moveInput;
        if (chargingJump)
        {
            chargeTimer += Time.deltaTime;

            float chargePercent = Mathf.Clamp01(
                chargeTimer / chargeTime
            );

            jumpPower = Mathf.Lerp(
                minJumpPower,
                maxJumpPower,
                chargePercent
            );

        }
    }

    private void HandleJump(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            StartCharging();
        }

        if (context.canceled)
        {
            ReleaseJump();
        }
    }

    private void StartCharging()
    {
        chargingJump = true;
        chargeTimer = 0f;
        jumpPower = minJumpPower;
    }

    private void ReleaseJump()
    {
        if (!chargingJump)
            return;

        chargingJump = false;

        float chargePercent = Mathf.Clamp01(chargeTimer / chargeTime);

        // Vertical jump strength
        float verticalPower = Mathf.Lerp(
            minJumpPower,
            maxJumpPower,
            chargePercent
        );

        // Horizontal strength based on charge
        float horizontalPower = Mathf.Lerp(
            minHorizontalPower,
            maxHorizontalPower,
            chargePercent
        );

        // Direction comes from the player's LEFT/RIGHT input
        float horizontalDirection = Mathf.Sign(moveInput.x);

        // If there is no left/right input, don't apply horizontal force
        if (Mathf.Abs(moveInput.x) < 0.01f)
        {
            horizontalDirection = 0f;
        }

        Vector3 force = new Vector3(
            horizontalDirection * horizontalPower,
            verticalPower,
            0f
        );

        rb.AddForce(force, ForceMode.Impulse);

        chargeTimer = 0f;
        jumpPower = minJumpPower;
    }
    
    private void OnDisable()
    {
        inputHandler.AnnounceJumpAction -= HandleJump;
    }
}
