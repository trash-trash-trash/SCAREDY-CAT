using System;
using System.Collections;
using UnityEngine;

public class PSAggroStand : PlantStateBase
{
    public float waitTime = 1.5f;

    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        plantBrain.ChangeState(PlantStates.Attacking);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}