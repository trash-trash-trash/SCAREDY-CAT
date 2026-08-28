using System.Collections;
using UnityEngine;

public class DogFightController : MonoBehaviour
{
    public PlayerBrain playerBrain;
    public DogBrain dogBrain;
    public DogFightTrigger dogFightTrigger;
    public HandOfGod handOfGod;

    public GameObject roofObj;

    private void Awake()
    {
        WaitForTrigger();
    }

    public void WaitForTrigger()
    {
        if(handOfGod.isPaused)
            handOfGod.UnpauseCountdown();
        
        roofObj.SetActive(false);
        dogFightTrigger.lookingForPlayer = true;

        dogFightTrigger.AnnouncePlayerDetected -= StartFight;
        dogFightTrigger.AnnouncePlayerDetected += StartFight;
    }

    public void StartFight()
    {
        handOfGod.PauseCountdown();
        
        dogFightTrigger.lookingForPlayer = false;
        dogFightTrigger.AnnouncePlayerDetected -= StartFight;

        roofObj.SetActive(true);

        StartCoroutine(WaitForPlayerToLand());
    }

    private IEnumerator WaitForPlayerToLand()
    {
        // Wait until the player lands
        while (!playerBrain.groundCheck.targetLayerDetected)
        {
            yield return null;
        }

        // Lock the player while the dog enters the arena
        playerBrain.ChangeState(PlayerStates.InMenu);

        dogBrain.AnnounceDogState -= GivePlayerControl;
        dogBrain.AnnounceDogState += GivePlayerControl;

        dogBrain.StartFight();
    }

    private void GivePlayerControl(DogStates newState)
    {
        if (newState != DogStates.AggroStand)
            return;

        dogBrain.AnnounceDogState -= GivePlayerControl;

        playerBrain.ChangeState(PlayerStates.Idle);

        playerBrain.AnnouncePlayerState -= IfPlayerDies;
        playerBrain.AnnouncePlayerState += IfPlayerDies;

        dogBrain.AnnounceDogState -= IfDogDies;
        dogBrain.AnnounceDogState += IfDogDies;
    }

    private void IfDogDies(DogStates dogState)
    {
        if (dogState != DogStates.Defeated)
            return;

        dogBrain.AnnounceDogState -= IfDogDies;
        playerBrain.AnnouncePlayerState -= IfPlayerDies;

        EndFight();
    }

    private void IfPlayerDies(PlayerStates playerState)
    {
        if (playerState != PlayerStates.Death)
            return;

        playerBrain.AnnouncePlayerState -= IfPlayerDies;

        ResetFight();
    }

    public void ResetFight()
    {
        roofObj.SetActive(false);

        dogBrain.ChangeState(DogStates.Idle);
        dogBrain.health.Res();
        WaitForTrigger();
    }

    private void EndFight()
    {
        roofObj.SetActive(false);

        handOfGod.ResetCountdown();
    }
}
