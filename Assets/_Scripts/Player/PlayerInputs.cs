using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private InputSystem_Actions controls;

    public Vector2 moveInput;
    public Vector2 lookInput;

    public event Action<Vector2> AnnounceLookVector2;
    public event Action<Vector2> AnnounceMoveVector2;

    public event Action<InputAction.CallbackContext> AnnounceAttackAction;
    public event Action<InputAction.CallbackContext> AnnounceInteractAction;
    public event Action<InputAction.CallbackContext> AnnounceCrouchAction;
    public event Action<InputAction.CallbackContext> AnnounceJumpAction;
    public event Action<InputAction.CallbackContext> AnnouncePreviousAction;
    public event Action<InputAction.CallbackContext> AnnounceNextAction;
    public event Action<InputAction.CallbackContext> AnnounceSprintAction;
    
    public event Action<InputAction.CallbackContext> AnnouncePause;


    private void Awake()
    {
        controls = new InputSystem_Actions();

        // Movement
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;

        // Look
        controls.Player.Look.performed += OnLook;
        controls.Player.Look.canceled += OnLook;

        // Actions
        controls.Player.Attack.performed += OnAttack;
        controls.Player.Attack.canceled += OnAttack;

        controls.Player.Interact.performed += OnInteract;
        controls.Player.Interact.canceled += OnInteract;

        controls.Player.Crouch.performed += OnCrouch;
        controls.Player.Crouch.canceled += OnCrouch;

        controls.Player.Jump.performed += OnJump;
        controls.Player.Jump.canceled += OnJump;

        controls.Player.Pause.performed += OnPause;
        controls.Player.Pause.canceled += OnPause;
    }


    private void OnEnable()
    {
        controls.Player.Enable();
    }

    // ------------------------------------------------------------------------
    // Movement
    // ------------------------------------------------------------------------

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moveInput = Vector2.zero;
        }

        AnnounceMoveVector2?.Invoke(moveInput);
    }


    // ------------------------------------------------------------------------
    // Look
    // ------------------------------------------------------------------------

    private void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            lookInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            lookInput = Vector2.zero;
        }

        AnnounceLookVector2?.Invoke(lookInput);
    }


    // ------------------------------------------------------------------------
    // Player Actions
    // ------------------------------------------------------------------------

    private void OnAttack(InputAction.CallbackContext context)
    {
        AnnounceAttackAction?.Invoke(context);
    }


    private void OnInteract(InputAction.CallbackContext context)
    {
        AnnounceInteractAction?.Invoke(context);
    }


    private void OnCrouch(InputAction.CallbackContext context)
    {
        AnnounceCrouchAction?.Invoke(context);
    }


    private void OnJump(InputAction.CallbackContext context)
    {
        AnnounceJumpAction?.Invoke(context);
    }


    private void OnPrevious(InputAction.CallbackContext context)
    {
        AnnouncePreviousAction?.Invoke(context);
    }


    private void OnNext(InputAction.CallbackContext context)
    {
        AnnounceNextAction?.Invoke(context);
    }


    private void OnSprint(InputAction.CallbackContext context)
    {
        AnnounceSprintAction?.Invoke(context);
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        AnnouncePause?.Invoke(context);
    }
    
    private void OnDisable()
    {
        controls.Player.Disable();

        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;

        controls.Player.Look.performed -= OnLook;
        controls.Player.Look.canceled -= OnLook;

        controls.Player.Attack.performed -= OnAttack;
        controls.Player.Attack.canceled -= OnAttack;

        controls.Player.Interact.performed -= OnInteract;
        controls.Player.Interact.canceled -= OnInteract;

        controls.Player.Crouch.performed -= OnCrouch;
        controls.Player.Crouch.canceled -= OnCrouch;

        controls.Player.Jump.performed -= OnJump;
        controls.Player.Jump.canceled -= OnJump;
    }

    private void OnDestroy()
    {
        controls.Dispose();
    }
}
