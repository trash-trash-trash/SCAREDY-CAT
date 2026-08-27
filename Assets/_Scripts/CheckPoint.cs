using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform teleportPoint;

    private void OnTriggerStay(Collider other)
    {
        PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

        if (playerBrain == null)
            return;

        if (playerBrain.mostRecentCheckPoint != this)
        {
            playerBrain.mostRecentCheckPoint = this;
        }
    }
}