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
      quitObj.SetActive(false);
   }
}
