using UnityEngine;

public class DogStateBase : MonoBehaviour
{
   public DogBrain dogBrain;
   
   public virtual void OnEnable()
   {
      dogBrain = GetComponentInParent<DogBrain>();
   }
}
