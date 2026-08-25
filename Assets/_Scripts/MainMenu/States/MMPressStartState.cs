using System;
using UnityEngine;

public class MMPressStartState : MonoBehaviour
{
   public GameObject pressStartObj;
   
   void OnEnable()
   {
      pressStartObj.SetActive(true);
   }

   private void OnDisable()
   {
      if(pressStartObj != null)
         pressStartObj.SetActive(false);
   }
}
