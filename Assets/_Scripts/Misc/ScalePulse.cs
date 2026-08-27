using System.Collections;
using UnityEngine;

public class ScalePulse : MonoBehaviour
{
    [SerializeField] private float maxMultiplier = 2f;
    [SerializeField] private float speed = 2f;

    private Vector3 startScale;

    private Coroutine pulseCoroutine;

    public bool pulsing { get; private set; }

    private void Awake()
    {
        startScale = transform.localScale;
    }

    public void StartPulse()
    {
        if (pulsing)
            return;

        pulsing = true;

        if (pulseCoroutine != null)
            StopCoroutine(pulseCoroutine);

        pulseCoroutine = StartCoroutine(Pulse());
    }

    public void EndPulse()
    {
        if (!pulsing)
            return;

        pulsing = false;

        if (pulseCoroutine != null)
        {
            StopCoroutine(pulseCoroutine);
            pulseCoroutine = null;
        }

        transform.localScale = startScale;
    }

    private IEnumerator Pulse()
    {
        while (pulsing)
        {
            float t = Mathf.PingPong(Time.time * speed, 1f);

            transform.localScale = Vector3.Lerp(
                startScale,
                startScale * maxMultiplier,
                t
            );

            yield return null;
        }

        transform.localScale = startScale;
        pulseCoroutine = null;
    }

    public void StartPulseOnce()
    {
        if (pulseCoroutine != null)
            StopCoroutine(pulseCoroutine);

        pulseCoroutine = StartCoroutine(PulseOnce());
    }

    private IEnumerator PulseOnce()
    {
        pulsing = false;

        // Normal -> Max
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;

            float percent = Mathf.Clamp01(t);

            transform.localScale = Vector3.Lerp(
                startScale,
                startScale * maxMultiplier,
                percent
            );

            yield return null;
        }

        // Max -> Normal
        t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;

            float percent = Mathf.Clamp01(t);

            transform.localScale = Vector3.Lerp(
                startScale * maxMultiplier,
                startScale,
                percent
            );

            yield return null;
        }

        transform.localScale = startScale;
        pulseCoroutine = null;
    }
}
