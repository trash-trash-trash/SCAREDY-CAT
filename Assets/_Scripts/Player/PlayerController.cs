using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [Header("Fiddle this for horizontal move speed")]
    [SerializeField] private float moveSpeed = 8f;

    public PlayerInputs playerInputs;
    
    public Rigidbody rb;
    private Vector2 moveInput;

    public GroundCheck groundCheck;
    public PlayerJump playerJump;
    
    private void Awake()
    {
        playerInputs.AnnounceMoveVector2 += HandleMoveInput;
    }

    private void HandleMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    private void FixedUpdate()
    {
        //can't move left to right in the air or while charging jump
        if (!groundCheck.isGrounded || playerJump.chargingJump)
            return;
        
        rb.linearVelocity = new Vector2(
            moveInput.x * moveSpeed,
            rb.linearVelocity.y
        );
    }

    private void OnDestroy()
    {
        playerInputs.AnnounceMoveVector2 -= HandleMoveInput;
    }
}
