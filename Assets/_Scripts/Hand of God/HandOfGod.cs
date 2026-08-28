using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HandOfGod : MonoBehaviour
{
    public Transform playerTransform;
    public Transform handOfGodTransform;


    public float yOffsetFollowing;
    public float zOffsetFollowing;
    
    public float currentTime;

    public float countdownEndTime;
    
    public float timeTilNextAppearance;
    
    private float minTimeTilAppearance = 120;
    private float maxTimeTilAppearance = 300;

    [SerializeField]
    private float godIsComingWarning = 30f;

    public event Action AnnounceWarning;
    public event Action<int> AnnounceCountdown;
    public event Action AnnounceArrival;

    public bool testing = false;

    private Coroutine countdownCoroutine;

    public bool isPaused = false;
    
    public float yOffsetClose = 10f;
    public float godDepartureDuration = 2f;

    private bool isDeparting;

    public Sprite handOfGod01;
    public Sprite handOfGod02;
    public Sprite handOfGod03;
    public SpriteRenderer spriteRenderer;
    
    private Coroutine departureCoroutine;

    public void StartCountdown()
    {
        if (countdownCoroutine != null)
            StopCoroutine(countdownCoroutine);

        if (testing)
            timeTilNextAppearance = 30;
        
        else
        {
            timeTilNextAppearance = Random.Range(
                minTimeTilAppearance,
                maxTimeTilAppearance
            );
        }
        
        //randomly pick sprite for variety
        spriteRenderer.sprite = Random.Range(0, 3) switch
        {
            0 => handOfGod01,
            1 => handOfGod02,
            _ => handOfGod03
        };
        
        //randomly flip left/right for variety
        if (Random.value < 0.5f)
            spriteRenderer.transform.eulerAngles = new Vector3(0, 180, 0);
        else
            spriteRenderer.transform.eulerAngles = new Vector3(0, 0, 0);

        countdownEndTime = Time.time + timeTilNextAppearance;

        isPaused = false;

        countdownCoroutine = StartCoroutine(Countdown());
    }


    private void Update()
    {
        float currentYOffset = yOffsetFollowing;

        if (!isDeparting && currentTime <= godIsComingWarning)
        {
            float progress = 1f - (currentTime / godIsComingWarning);

            currentYOffset = Mathf.Lerp(
                yOffsetFollowing,
                yOffsetClose,
                progress
            );
        }

        handOfGodTransform.position = new Vector3(
            playerTransform.position.x,
            playerTransform.position.y + currentYOffset,
            playerTransform.position.z + zOffsetFollowing
        );

        if (!isPaused)
        {
            currentTime = Mathf.Max(
                0f,
                countdownEndTime - Time.time
            );
        }
    }



    public void PauseCountdown()
    {
        if (isPaused)
            return;

        isPaused = true;

        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
    }

    public void UnpauseCountdown()
    {
        if (!isPaused)
            return;

        isPaused = false;

        countdownCoroutine = StartCoroutine(Countdown());
    }

    public void ResetCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }

        isPaused = false;

        StartCountdown();
    }

    private IEnumerator Countdown()
    {
        // Countdown until 30 second warning
        float remainingTime = countdownEndTime - Time.time;

        if (remainingTime > godIsComingWarning)
        {
            yield return new WaitForSeconds(
                remainingTime - godIsComingWarning
            );
        }

        // 30 second warning
        AnnounceWarning?.Invoke();

        // Countdown
        for (int i = (int)godIsComingWarning; i > 0; i--)
        {
            AnnounceCountdown?.Invoke(i);

            float progress = 1f - (i / godIsComingWarning);
            float delay = Mathf.Lerp(1.5f, 0.3f, progress * progress);

            yield return new WaitForSeconds(delay);
        }

        currentTime = 0f;

        AnnounceArrival?.Invoke();
        
        StartCoroutine(MoveHandAway());

        StartCountdown();
    }
    
    private IEnumerator MoveHandAway()
    {
        isDeparting = true;

        float elapsed = 0f;

        while (elapsed < godDepartureDuration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(
                elapsed / godDepartureDuration
            );

            float currentYOffset = Mathf.Lerp(
                yOffsetClose,
                yOffsetFollowing,
                progress
            );

            handOfGodTransform.position = new Vector3(
                playerTransform.position.x,
                playerTransform.position.y + currentYOffset,
                playerTransform.position.z + zOffsetFollowing
            );

            yield return null;
        }

        isDeparting = false;
    }
}
