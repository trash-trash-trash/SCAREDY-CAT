using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
   public PlayerBrain playerBrain;
   public HandOfGod handOfGod;
   public MainMenu mainMenu;

   public bool startedGame = false;

   public float fadeTime = 1f;
   public float displayTime = 1f;

   public TMP_Text godIsComingText;

   void Start()
   {
      handOfGod.AnnounceWarning += FadeTextInOut;
      mainMenu.AnnounceMainMenuState += StartGame;
   }

   private void FadeTextInOut()
   {
      StartCoroutine(FadeTextCoro());
   }

   private IEnumerator FadeTextCoro()
   {
      //fade in
      for (float t = 0; t < fadeTime; t += Time.deltaTime)
      {
         godIsComingText.alpha = t / fadeTime;
         yield return null;
      }

      godIsComingText.alpha = 1f;

      //display
      yield return new WaitForSeconds(displayTime);

      //fade out
      for (float t = 0; t < fadeTime; t += Time.deltaTime)
      {
         godIsComingText.alpha = 1f - (t / fadeTime);
         yield return null;
      }

      godIsComingText.alpha = 0f;
   }
   
   private void StartGame(MainMenuStates newState)
   {
      if(!startedGame)
      {
         if (newState == MainMenuStates.InGame)
         {
            handOfGod.StartCountdown();
            startedGame = true;
         }
      }
   }

   void OnDisable()
   {
      handOfGod.AnnounceWarning -= FadeTextInOut;
      mainMenu.AnnounceMainMenuState -= StartGame;
   }
}
