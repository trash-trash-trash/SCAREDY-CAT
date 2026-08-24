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
      mainMenuObj.SetActive(false);
   }
}
