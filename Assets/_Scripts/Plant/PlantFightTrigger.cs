using System;
using UnityEngine;

public class PlantFightTrigger : MonoBehaviour
{
    public event Action<Transform> AnnouncePlayerDetected;
    public event Action AnnouncePlayerLost;

    private void OnTriggerStay(Collider other)
    {
        PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

        if (playerBrain == null)
            return;

        AnnouncePlayerDetected?.Invoke(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

        if (playerBrain == null)
            return;

        AnnouncePlayerLost?.Invoke();
    }
}