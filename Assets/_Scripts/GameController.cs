using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
   //mixing model / view but fuck it game jam we ball
   
   public PlayerBrain playerBrain;
   public PlayerInputs playerInput;
   public HandOfGod handOfGod;
   public MainMenu mainMenu;
   public YouDied youDied;

   public bool startedGame = false;

   public float fadeTime = 1f;
   public float displayTime = 1f;

   public TMP_Text godIsComingText;

   public bool paused = false;
   public GameObject pausedObj;

   void Start()
   {
      handOfGod.AnnounceWarning += FadeTextInOut;
      handOfGod.AnnounceArrival += CheckPlayerHidden;
      mainMenu.AnnounceMainMenuState += StartGame;
      playerInput.AnnouncePause += PauseUnpause;
      playerBrain.health.AnnounceDeath += BENDROWNED;
   }

   private void BENDROWNED()
   {
      youDied.BENDROWNED();
   }

   public void Reset()
   {
      youDied.BENGOTCPR();
      
      playerBrain.Reset();
   }

   private void CheckPlayerHidden()
   {
      if (playerBrain.currentState != PlayerStates.Hiding)
      {
         playerBrain.health.ChangeHealth(-777);
      }
   }

   private void PauseUnpause(InputAction.CallbackContext context)
   {
      if(context.performed)
      {
         paused = !paused;
         if (paused)
         {
            pausedObj.SetActive(true);
            Time.timeScale = 0f;
         }
         else
         {
            pausedObj.SetActive(false);
            Time.timeScale = 1f;
         }
      }
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
      playerInput.AnnouncePause -= PauseUnpause;
      handOfGod.AnnounceWarning -= FadeTextInOut;
      mainMenu.AnnounceMainMenuState -= StartGame;
      playerBrain.health.AnnounceDeath -= BENDROWNED;
   }
}
