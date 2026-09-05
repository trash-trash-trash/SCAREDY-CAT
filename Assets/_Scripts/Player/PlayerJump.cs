using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings You Can Fiddle With")]
    public float minJumpPower = 5f;
    public float maxJumpPower = 15f;
    
    [SerializeField] private float minHorizontalPower = 2f;
    [SerializeField] private float maxHorizontalPower = 10f;
    
    [SerializeField] private float chargeTime = 1f;
    
    public bool canJump = false;

    private Vector2 moveInput;

    public bool chargingJump;
    public float chargeTimer;
    public float jumpPower;
    
   [SerializeField] private Rigidbody rb;
    public PlayerInputs inputHandler;
    public PlayerMovement playerMovement;

    //true for charging, false for releasing
    
    public bool invertedJump = false;
    
    public event Action<bool> AnnounceChargingJump; 
    
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

    public void FlipCanJump(bool input)
    {
        canJump = input;
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (!canJump)
            return;
        
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
        AnnounceChargingJump?.Invoke(true);
    }

    private void ReleaseJump()
    {
        if (!chargingJump)
            return;

        chargingJump = false;
        
        float verticalMultiplier = 1.5f;

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
        // IE, jump straight up
        //increase vertical power
        if (Mathf.Abs(moveInput.x) < 0.01f)
        {
            horizontalDirection = 0f;
            verticalMultiplier = 1.8f;
        }

        float verticalDirection = invertedJump ? -1f : 1f;

        Vector3 force = new Vector3(
            horizontalDirection * horizontalPower,
            verticalPower * verticalMultiplier * verticalDirection,
            0f
        );

        rb.AddForce(force, ForceMode.Impulse);

        chargeTimer = 0f;
        jumpPower = minJumpPower;
        
        AnnounceChargingJump?.Invoke(false);
    }

    public void FlipInvertedJump(bool input)
    {
        invertedJump = input;
    }
    
    private void OnDisable()
    {
        inputHandler.AnnounceJumpAction -= HandleJump;
    }
}
