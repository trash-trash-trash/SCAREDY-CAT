using System;
using System.Collections;
using UnityEngine;

public class HandOfGod : MonoBehaviour
{
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

    public void StartCountdown()
    {
        if (countdownCoroutine != null)
            StopCoroutine(countdownCoroutine);

        if (testing)
            timeTilNextAppearance = 30;
        else
        {
            timeTilNextAppearance = UnityEngine.Random.Range(
                minTimeTilAppearance,
                maxTimeTilAppearance
            );
        }

        countdownEndTime = Time.time + timeTilNextAppearance;

        isPaused = false;

        countdownCoroutine = StartCoroutine(Countdown());
    }

    private void Update()
    {
        if (!isPaused)
        {
            currentTime = Mathf.Max(0f, countdownEndTime - Time.time);
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

        StartCountdown();
    }
}
