using System.Collections;
using UnityEngine;

public class RotateDoor : MonoBehaviour
{
    public Health health;

    [SerializeField] private float rotateDuration = 0.8f;

    private void Awake()
    {
        health.AnnounceDeath += Rotate;
    }

    public void Rotate()
    {
        StartCoroutine(RotateRoutine());
    }

    private IEnumerator RotateRoutine()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, 90f, 0f);

        float elapsed = 0f;

        while (elapsed < rotateDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / rotateDuration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            yield return null;
        }

        transform.rotation = endRotation;
    }

    private void OnDisable()
    {
        health.AnnounceDeath -= Rotate;
    }
}