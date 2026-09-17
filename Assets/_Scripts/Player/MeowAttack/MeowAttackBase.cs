using UnityEngine;

public class MeowAttackBase : MonoBehaviour
{
   public MeowAttackBrain attackBrain;

   public virtual void OnEnable()
   {
      attackBrain = GetComponentInParent<MeowAttackBrain>();
   }
}
