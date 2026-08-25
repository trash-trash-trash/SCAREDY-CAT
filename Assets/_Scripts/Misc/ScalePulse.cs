using System.Collections;
using UnityEngine;

public class ScalePulse : MonoBehaviour
{
    public float maxMultiplier = 2f;
    public float speed = 2f;

    private Vector3 startScale;

    public bool pulsing = false;

    void Start()
    {
        startScale = transform.localScale;
    }

    public void StartPulseOnce()
    {
        StartCoroutine(PulseOnce());
    }

    private IEnumerator PulseOnce()
    {
        // Normal -> Max
        float
            t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            float percent = Mathf.Clamp01(t);
            transform.localScale = Vector3.Lerp(startScale, startScale * maxMultiplier, percent);
            yield return null;
        } // Max -> Normal

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            float percent = Mathf.Clamp01(t);
            transform.localScale = Vector3.Lerp(startScale * maxMultiplier, startScale, percent);
            yield return null;
        }

        transform.localScale = startScale;
    }

    void Update()
    {
        if (!pulsing)
            return;

        float t = Mathf.PingPong(Time.time * speed, 1f);
        transform.localScale = startScale * Mathf.Lerp(1f, maxMultiplier, t);
    }
}