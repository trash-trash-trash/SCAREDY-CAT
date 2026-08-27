using System.Collections.Generic;
using UnityEngine;

public class DogView : MonoBehaviour
{
   public DogBrain dogBrain;
   
   public Animator animator;
   public AnimationClip idleClip;
   public AnimationClip chargeClip;
   public AnimationClip jumpClip;
   public AnimationClip defeatedClip;
   
   public Dictionary<DogStates, AnimationClip> statesDict = new Dictionary<DogStates, AnimationClip>();

   public void OnEnable()
   {
      statesDict.Add(DogStates.Idle, idleClip);
      statesDict.Add(DogStates.AggroStand, chargeClip);
      statesDict.Add(DogStates.AttackJump, jumpClip);
      statesDict.Add(DogStates.AttackHorizontal, idleClip);
      statesDict.Add(DogStates.Defeated, defeatedClip);

      dogBrain.AnnounceDogState += SetClip;
   }

   private void SetClip(DogStates obj)
   {
      if (statesDict.TryGetValue(obj, out AnimationClip clip))
      {
         animator.Play(clip.name);
      }
   }

   void OnDisable()
   {
      dogBrain.AnnounceDogState -= SetClip;
   }
}
