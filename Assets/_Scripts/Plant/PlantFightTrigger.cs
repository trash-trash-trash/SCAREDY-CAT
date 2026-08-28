using System;
using UnityEngine;

public class PlantFightTrigger : MonoBehaviour
{
    public bool lookingForPlayer = true;

    public event Action<Transform> AnnouncePlayerDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (!lookingForPlayer)
            return;

        PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

        if (playerBrain == null)
            return;

        lookingForPlayer = false;

        AnnouncePlayerDetected?.Invoke(other.transform);
    }
}