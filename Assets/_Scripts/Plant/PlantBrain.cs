using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlantStates
{
    Idle,
    AggroStand,
    Attacking,
    Defeated
}

public class PlantBrain : MonoBehaviour
{
    public PlantStates currentState = PlantStates.Idle;

    private GameObject prevObj;

    public GameObject idleObj;
    public GameObject aggroStandObj;
    public GameObject attackingObj;
    public GameObject defeatedObj;

    public Dictionary<PlantStates, GameObject> statesDict =
        new Dictionary<PlantStates, GameObject>();

    public event Action<PlantStates> AnnouncePlantState;

    void Awake()
    {
        statesDict.Add(PlantStates.Idle, idleObj);
        statesDict.Add(PlantStates.AggroStand, aggroStandObj);
        statesDict.Add(PlantStates.Attacking, attackingObj);
        statesDict.Add(PlantStates.Defeated, defeatedObj);

        ChangeState(PlantStates.Idle);
    }

    public void ChangeState(PlantStates newState)
    {
        if (statesDict.TryGetValue(newState, out GameObject stateObj))
        {
            if (prevObj != null)
                prevObj.SetActive(false);

            stateObj.SetActive(true);

            prevObj = stateObj;
            currentState = newState;

            AnnouncePlantState?.Invoke(currentState);
        }
    }
}