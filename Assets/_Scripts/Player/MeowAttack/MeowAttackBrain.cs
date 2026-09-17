using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public enum MeowAttackState
{
    Idle,
    Charging,
    Attacking,
    Disabled
}

public class MeowAttackBrain : MonoBehaviour
{
    public MeowAttackState currentState;
    public PlayerAttack playerAttack;
    
    public PlayerInputs inputHandler;
    
    public GameObject idleObj;
    public GameObject chargingObj;
    public GameObject attackingObj;
    public GameObject disabledObj;

    private GameObject prevStateObj;

    public Dictionary<MeowAttackState, GameObject> meowAttackStatesDict = new Dictionary<MeowAttackState, GameObject>();

    void Awake()
    {
        meowAttackStatesDict.Add(MeowAttackState.Idle, idleObj);
        meowAttackStatesDict.Add(MeowAttackState.Charging, chargingObj);
        meowAttackStatesDict.Add(MeowAttackState.Attacking, attackingObj);
        meowAttackStatesDict.Add(MeowAttackState.Disabled, disabledObj);
        
        ChangeState(MeowAttackState.Idle);
    }

    public void ChangeState(MeowAttackState newState)
    {
        if (!meowAttackStatesDict.TryGetValue(newState, out GameObject newStateObj))
            return;

        if (prevStateObj == newStateObj)
            return;

        // Disable previous state
        if (prevStateObj != null)
        {
            prevStateObj.SetActive(false);
        }

        // Enable new state
        newStateObj.SetActive(true);

        prevStateObj = newStateObj;
        currentState = newState;
    }

}