using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    Idle,
    InMenu,
    Walking,
    ChargingAttack,
    Attacking,
    ChargingJump,
    Jumping,
    Falling,
    StickingToWall,
    ChargingWallJump,
    ClimbingUpLedge,
    Hiding,
    Unhiding
}

public class PlayerBrain : MonoBehaviour
{
    public Health health;
    
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public PlayerJump playerJump;
    
    public LayerCheck groundCheck;
    public LayerCheck leftWallCheck;
    public LayerCheck rightWallCheck;

    public LayerCheck currentWallCheck;

    public Rigidbody rb;
    
    public Transform ledgeTarget;
    
    public Vector3 positionBeforeHiding = Vector3.zero;

    public bool canHide = false;
    public bool hiding = false;

    public bool leftWall = false;
    
    public PlayerStates currentState = PlayerStates.Idle;
    private GameObject prevObj;
    [Header("Player State Objects")] public GameObject idleObj;
    public GameObject inMenuObj;
    public GameObject walkingObj;
    public GameObject chargingAttackObj;
    public GameObject attackObj;
    public GameObject chargingJumpObj;
    public GameObject jumpingObj;
    public GameObject fallingObj;
    public GameObject stickingToWallObj;
    public GameObject chargingWallJumpObj;
    public GameObject climbingUpLedgeObj;
    public GameObject hidingObj;
    public GameObject unhidingObj;

    public Dictionary<PlayerStates, GameObject> statesDict =
        new Dictionary<PlayerStates, GameObject>();

    public HidingSpot currentHidingSpot;
    
    public event Action<PlayerStates> AnnouncePlayerState;

    public event Action<bool> AnnounceCanHide;

    public bool naomiTesting = false;

    public bool testing = false;

    private void Awake()
    {
        statesDict.Add(PlayerStates.Idle, idleObj);
        statesDict.Add(PlayerStates.InMenu, inMenuObj);
        statesDict.Add(PlayerStates.ChargingAttack, chargingAttackObj);
        statesDict.Add(PlayerStates.Attacking, attackObj);
        statesDict.Add(PlayerStates.Walking, walkingObj);
        statesDict.Add(PlayerStates.ChargingJump, chargingJumpObj);
        statesDict.Add(PlayerStates.Jumping, jumpingObj);
        statesDict.Add(PlayerStates.Falling, fallingObj);
        statesDict.Add(PlayerStates.StickingToWall, stickingToWallObj);
        statesDict.Add(PlayerStates.ChargingWallJump,  chargingWallJumpObj);
        statesDict.Add(PlayerStates.ClimbingUpLedge, climbingUpLedgeObj);
        statesDict.Add(PlayerStates.Hiding, hidingObj);
        statesDict.Add(PlayerStates.Unhiding, unhidingObj);
        
        if(naomiTesting)
            ChangeState(PlayerStates.Idle);
        
        else
            ChangeState(PlayerStates.InMenu);
    }

    public void ChangeState(PlayerStates newState)
    {
        if (statesDict.TryGetValue(newState, out GameObject stateObj))
        {
            if (prevObj != null)
                prevObj.SetActive(false);

            stateObj.SetActive(true);

            prevObj = stateObj;
            currentState = newState;

            AnnouncePlayerState?.Invoke(currentState);
        }
    }
    
    public void StartLedgeClimb(Transform target)
    {
        ledgeTarget = target;
        ChangeState(PlayerStates.ClimbingUpLedge);
    }

    public void FlipCanHide(bool input)
    {
        canHide = input;
        AnnounceCanHide?.Invoke(canHide);
    }
}