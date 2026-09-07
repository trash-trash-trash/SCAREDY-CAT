using System;
using System.Collections.Generic;
using UnityEngine;

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
    
    public GameObject idleObj;
    public GameObject chargingObj;
    public GameObject attackingObj;

    private GameObject prevStateObj;

    public Dictionary<MeowAttackState, GameObject> meowAttackStatesDict = new Dictionary<MeowAttackState, GameObject>();

    public bool canAttack = true;

    void Awake()
    {
        meowAttackStatesDict.Add(MeowAttackState.Idle, idleObj);
        meowAttackStatesDict.Add(MeowAttackState.Charging, chargingObj);
        meowAttackStatesDict.Add(MeowAttackState.Attacking, attackingObj);
    }

    public void ChangeState(MeowAttackState newState)
    {
        GameObject value;
        if (meowAttackStatesDict.TryGetValue(newState, out value))
        {
            currentState = newState;
            value.SetActive(true);

            if (prevStateObj != null)
            {
                prevStateObj.SetActive(false);
                prevStateObj = value;
            }
        }
    }
}