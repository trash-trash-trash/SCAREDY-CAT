using UnityEngine;

public class MMQuitState : MainMenuStateBase
{
   public GameObject quitObj;

   public override void OnEnable()
   {
      base.OnEnable();
      quitObj.SetActive(true);
   }

   void OnDisable()
   {
      if(quitObj!=null)
         quitObj.SetActive(false);
   }
}
