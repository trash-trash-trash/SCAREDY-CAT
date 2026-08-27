using System;
using UnityEngine;

public class DogFightTrigger : MonoBehaviour
{
    public bool lookingForPlayer = true;

    public event Action AnnouncePlayerDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (!lookingForPlayer)
            return;

        PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

        if (playerBrain == null)
            return;

        lookingForPlayer = false;

        AnnouncePlayerDetected?.Invoke();
    }
}