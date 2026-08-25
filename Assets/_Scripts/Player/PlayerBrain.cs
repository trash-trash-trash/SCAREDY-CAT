using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    Idle,
    InMenu,
    Walking,
    ChargingJump,
    Jumping,
    Falling,
    StickingToWall,
    ChargingWallJump,
    ClimbingUpLedge
}

public class PlayerBrain : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerJump playerJump;
    
    public LayerCheck groundCheck;
    public LayerCheck leftWallCheck;
    public LayerCheck rightWallCheck;

    public LayerCheck currentWallCheck;

    public Rigidbody rb;
    
    public Transform ledgeTarget;
    
    public PlayerStates currentState = PlayerStates.Idle;
    private GameObject prevObj;
    [Header("Player State Objects")] public GameObject idleObj;
    public GameObject inMenuObj;
    public GameObject walkingObj;
    public GameObject chargingJumpObj;
    public GameObject jumpingObj;
    public GameObject fallingObj;
    public GameObject stickingToWallObj;
    public GameObject chargingWallJumpObj;
    public GameObject climbingUpLedgeObj;

    public Dictionary<PlayerStates, GameObject> statesDict =
        new Dictionary<PlayerStates, GameObject>();

    public event Action<PlayerStates> AnnouncePlayerState;

    public bool testing = false;

    private void OnEnable()
    {
        statesDict.Add(PlayerStates.Idle, idleObj);
        statesDict.Add(PlayerStates.InMenu, inMenuObj);
        statesDict.Add(PlayerStates.Walking, walkingObj);
        statesDict.Add(PlayerStates.ChargingJump, chargingJumpObj);
        statesDict.Add(PlayerStates.Jumping, jumpingObj);
        statesDict.Add(PlayerStates.Falling, fallingObj);
        statesDict.Add(PlayerStates.StickingToWall, stickingToWallObj);
        statesDict.Add(PlayerStates.ChargingWallJump,  chargingWallJumpObj);
        statesDict.Add(PlayerStates.ClimbingUpLedge, climbingUpLedgeObj);
        
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
}