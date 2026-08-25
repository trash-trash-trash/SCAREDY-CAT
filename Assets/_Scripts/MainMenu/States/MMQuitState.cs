using UnityEngine;

public class MMQuitState : MonoBehaviour
{
   public GameObject quitObj;

   void OnEnable()
   {
      quitObj.SetActive(true);
   }

   void OnDisable()
   {
      if(quitObj!=null)
         quitObj.SetActive(false);
   }
}
