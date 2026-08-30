using System;
using System.Collections;
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
    Unhiding,
    TakeDamage,
    Death
}

public class PlayerBrain : MonoBehaviour
{
    public CheckPoint originalCheckPoint;
    public CheckPoint mostRecentCheckPoint;
    
    public Health health;

    public PlayerLives playerLives;
    
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
    public GameObject takeDamageObj;
    public GameObject deathObj;
    
    public Dictionary<PlayerStates, GameObject> statesDict =
        new Dictionary<PlayerStates, GameObject>();

    public HidingSpot currentHidingSpot;
    
    public event Action<PlayerStates> AnnouncePlayerState;

    public event Action<bool> AnnounceCanHide;

    public event Action<bool> AnnounceHidden;

    public event Action AnnounceHardFlip;

    public event Action<bool, string> AnnounceCanInvestigate;

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
        statesDict.Add(PlayerStates.TakeDamage, takeDamageObj);
        statesDict.Add(PlayerStates.Death, deathObj);

        health.AnnounceTakeDamage += TakeDamage;
        health.AnnounceDeath += Dead;
        
        if(naomiTesting)
            ChangeState(PlayerStates.Idle);
        
        else
            ChangeState(PlayerStates.InMenu);
    }

    private void Dead()
    {
        ChangeState(PlayerStates.Death);
    }

    private void TakeDamage()
    {
        ChangeState(PlayerStates.TakeDamage);
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

    public void Reset()
    {
        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        if (mostRecentCheckPoint != null)
        {
            transform.position = mostRecentCheckPoint.teleportPoint.position;
        }
        else
        {
            transform.position = originalCheckPoint.teleportPoint.position;
        }

        // Wait until the transform position has actually been applied
        yield return null;

        ChangeState(PlayerStates.Idle);
        health.Res();
    }
    
    public void StartLedgeClimb(Transform target)
    {
        ledgeTarget = target;
        ChangeState(PlayerStates.ClimbingUpLedge);
    }
    
    public void FlipCanInvestigate(bool input, string newText)
    {
        AnnounceCanInvestigate?.Invoke(input, newText);
    }

    public void FlipCanHide(bool input)
    {
        canHide = input;
        AnnounceCanHide?.Invoke(canHide);
    }

    public void IAmHidingNow(bool input)
    {
        AnnounceHidden?.Invoke(input);
    }

    public void HardFlip()
    {
        AnnounceHardFlip?.Invoke();
    }

    private void OnDestroy()
    {
        health.AnnounceTakeDamage -= TakeDamage;
        health.AnnounceDeath -= Dead;
    }
}