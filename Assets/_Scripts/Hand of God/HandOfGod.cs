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

    public void StartCountdown()
    {
        if (testing)
            timeTilNextAppearance = 40;
        
        else
        {
            timeTilNextAppearance = UnityEngine.Random.Range(
                minTimeTilAppearance,
                maxTimeTilAppearance
            );
        }
        
        countdownEndTime = Time.time + timeTilNextAppearance;

        StartCoroutine(Countdown());
    }
    
    private void Update()
    {
        currentTime = Mathf.Max(0f, countdownEndTime - Time.time);
    }
    

    private IEnumerator Countdown()
    {
        //countdown til 30 second warning
        yield return new WaitForSeconds(
            timeTilNextAppearance - godIsComingWarning
        );

        //30 second warning
        AnnounceWarning?.Invoke();

        
        //more steps increasingly
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