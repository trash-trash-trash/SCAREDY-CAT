using System.Collections;
using UnityEngine;

public class PSAttack : PlayerStateBase
{
    public float attackDuration = 0.3f;
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.meow.PlayRandom();
        StartCoroutine(AttackTime());
    }

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(0.3f);
        playerBrain.ChangeState(PlayerStates.Idle);
    }
}
