using System;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public ScalePulse scalePulse;
    
    public bool playerDetected = false;
    private void OnTriggerStay(Collider other)
    {
        PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

        if (playerBrain == null)
            return;

        playerDetected = true;

        playerBrain.currentHidingSpot = this;
        
        if(!playerBrain.canHide) 
            playerBrain.FlipCanHide(true);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

        if (playerBrain == null)
            return;
        
        playerBrain.currentHidingSpot = null;

        playerDetected = false;
        playerBrain.FlipCanHide(false);
    }

    public void Hide()
    {
        scalePulse.StartPulseOnce();
    }
}