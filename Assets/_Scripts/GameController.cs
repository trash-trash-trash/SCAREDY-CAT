using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
   //mixing model / view but fuck it game jam we ball

   public List<PlantBrain> plantHealthList = new List<PlantBrain>();
   
   public DogFightController dogFight;
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

   public float endGameWait = 5f;

   public bool endedGame = false;

   void Start()
   {
      handOfGod.AnnounceWarning += FadeTextInOut;
      handOfGod.AnnounceArrival += CheckPlayerHidden;
      mainMenu.AnnounceMainMenuState += StartGame;
      playerInput.AnnouncePause += PauseUnpause;
      playerBrain.health.AnnounceDeath += BENDROWNED;
   }

   public void GameOver()
   {
      Debug.Log("GAME OVER BUTTON");
      StartCoroutine(GameOverCoro());
   }

   IEnumerator GameOverCoro()
   {
      Debug.Log("SUP");
      startedGame = false;
      youDied.youDiedObj.SetActive(false);
      playerBrain.health.Res();
      playerBrain.playerLives.currentLives = 9;
      playerBrain.ChangeState(PlayerStates.InMenu);
      handOfGod.PauseCountdown();
      dogFight.ResetFight();
      
      yield return null;
      
      playerBrain.rb.linearVelocity = Vector3.zero;
      playerBrain.rb.angularVelocity = Vector3.zero;

      playerBrain.rb.position =
         playerBrain.originalCheckPoint.teleportPoint.position;
      
      yield return null;
      mainMenu.ChangeState(MainMenuStates.PressStart);

      endedGame = false;
   }

   private void BENDROWNED()
   {
      handOfGod.PauseCountdown();
      youDied.BENDROWNED();

      foreach (PlantBrain pb in plantHealthList)
      {
         if(!pb.health.isAlive)
            pb.plantView.transform.position += new Vector3(0, 3f, 0);

         pb.health.Res();
         pb.ChangeState(PlantStates.Idle);
      }
   }

   public void Reset()
   {
      handOfGod.StartCountdown();
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
            playerBrain.ChangeState(PlayerStates.Idle);
            startedGame = true;
         }
      }
   }

   public void EndGame()
   {
      if(!endedGame)
         StartCoroutine(EndGameCoro());
   }

   IEnumerator EndGameCoro()
   {
      endedGame = true;
      yield return new WaitForSeconds(endGameWait);
      handOfGod.GrabPlayerCoro();
   }

   void OnDisable()
   {
      playerInput.AnnouncePause -= PauseUnpause;
      handOfGod.AnnounceWarning -= FadeTextInOut;
      mainMenu.AnnounceMainMenuState -= StartGame;
      playerBrain.health.AnnounceDeath -= BENDROWNED;
   }
}
