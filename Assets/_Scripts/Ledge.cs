using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    public Transform associatedGroundPoint;
    public List<Transform> climbPoints = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

        if (playerBrain == null)
            return;

        if (playerBrain.currentState == PlayerStates.StickingToWall ||
            playerBrain.currentState == PlayerStates.Jumping ||
            playerBrain.currentState == PlayerStates.StickingToRoof)
        {
            playerBrain.StartLedgeClimb(this);
        }
    }
}