using System.Collections;
using UnityEngine;

public class DSIdle : DogStateBase
{
   public float fadeTime = 0.5f;
   
   public override void OnEnable()
   {
      base.OnEnable();
      dogBrain.spriteRenderer.color = new Color(dogBrain.spriteRenderer.color.r, dogBrain.spriteRenderer.color.g, dogBrain.spriteRenderer.color.b, 0);
      dogBrain.AnnounceFightStarted += Activate;
   }

   private void Activate()
   {
      if (Random.value < 0.5f)
      {
         dogBrain.firstPoint = dogBrain.pointA.position;
         dogBrain.secondPoint = dogBrain.pointB.position;
      }
      else
      {
         dogBrain.firstPoint = dogBrain.pointB.position;
         dogBrain.secondPoint = dogBrain.pointA.position;
      }
      
      dogBrain.transform.position = dogBrain.firstPoint;
      StartCoroutine(FadeIn());
   }

   private IEnumerator FadeIn()
   {
      float elapsed = 0f;

      while (elapsed < fadeTime)
      {
         elapsed += Time.deltaTime;

         float t = elapsed / fadeTime;

         Color color = dogBrain.spriteRenderer.color;
         color.a = Mathf.Lerp(0f, dogBrain.originalColor.a, t);
         dogBrain.spriteRenderer.color = color;

         yield return null;
      }

      dogBrain.spriteRenderer.color = dogBrain.originalColor;
      dogBrain.ChangeState(DogStates.AggroStand);
   }

   void OnDisable()
   {
      dogBrain.AnnounceFightStarted -= Activate;
   }
}
