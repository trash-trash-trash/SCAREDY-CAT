using System;
using UnityEngine;

public class MMMMState : MonoBehaviour
{
   public GameObject mainMenuObj;
   private void OnEnable()
   {
      mainMenuObj.SetActive(true);
   }

   private void OnDisable()
   {
      if(mainMenuObj != null)
         mainMenuObj.SetActive(false);
   }
}
