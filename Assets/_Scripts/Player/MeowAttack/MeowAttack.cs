using System.Collections;
using UnityEngine;

public class MeowAttack : MeowAttackBase
{
   public override void OnEnable()
   {
      base.OnEnable();
      attackBrain.playerAttack.ReleaseAttack();
      StartCoroutine(WaitForAttackToEnd(attackBrain.playerAttack.attackVisualTime));
   }

   IEnumerator WaitForAttackToEnd(float waitTime)
   {
      yield return new WaitForSeconds(waitTime);
      attackBrain.ChangeState(MeowAttackState.Idle);
   }
}
