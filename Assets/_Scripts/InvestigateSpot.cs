using System;
using UnityEngine;

public class InvestigateSpot : MonoBehaviour
{
    public bool revealed = false;

    public string investigateText;

    private void OnTriggerStay(Collider other)
    {
        if (!revealed)
            return;

        PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

        if (playerBrain == null)
            return;

        playerBrain.FlipCanInvestigate(true, investigateText);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

        if (playerBrain == null)
            return;

        playerBrain.FlipCanInvestigate(false, investigateText);
    }
}