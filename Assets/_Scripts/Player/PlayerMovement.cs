using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Fiddle this for horizontal move speed")]
    [SerializeField] private float moveSpeed = 8f;

    public PlayerInputs playerInputs;
    
    public Rigidbody rb;
    public Vector2 moveInput;

    public bool readingLeftRight = false;
    public bool readingUpDown = false;
    
    public float facingDirection = 1f;
    
    public PlayerJump playerJump;
    
    private void Awake()
    {
        //ignore ledges
        Physics.IgnoreLayerCollision(0,10);
        playerInputs.AnnounceMoveVector2 += HandleMoveInput;
    }

    private void HandleMoveInput(Vector2 input)
    {
        moveInput = input;
        
        if (moveInput.x > 0.01f)
            facingDirection = 1f;
        else if (moveInput.x < -0.01f)
            facingDirection = -1f;
    }

    private void FixedUpdate()
    {
        //walkin' around
        if (readingLeftRight)
        {

            rb.linearVelocity = new Vector2(
                moveInput.x * moveSpeed,
                rb.linearVelocity.y
            );
        }

        //walkin' around walls
        if (readingUpDown)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                moveInput.y * moveSpeed
            );
        }
    }

    private void OnDestroy()
    {
        playerInputs.AnnounceMoveVector2 -= HandleMoveInput;
    }
}
