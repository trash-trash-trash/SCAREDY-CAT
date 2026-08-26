using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeStrength = 0.2f;

    private Vector3 originalPosition;

    public HandOfGod handOfGod;

    void Start()
    {
        handOfGod.AnnounceCountdown += InitiateShake;
    }

    private void InitiateShake(int obj)
    {
        Shake();
    }

    public void Shake()
    {
        originalPosition = transform.localPosition;

        StopAllCoroutines();
        StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        float timer = 0f;

        while (timer < shakeDuration)
        {
            Vector2 offset = Random.insideUnitCircle * shakeStrength;

            transform.localPosition = originalPosition + new Vector3(
                offset.x,
                offset.y,
                0f
            );

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}