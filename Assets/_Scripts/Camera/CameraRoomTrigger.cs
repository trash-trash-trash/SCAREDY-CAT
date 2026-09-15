using UnityEngine;

public class CameraRoomTrigger : MonoBehaviour
{
    public enum TrackAxis
    {
        LeftRight,
        UpDown
    }

    public Transform cameraPosition;

    public bool isTracker;
    public TrackAxis trackAxis;

    public void OnTriggerStay(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        if (CameraController.Instance.target != cameraPosition)
        {
            CameraController.Instance.MoveToRoom(
                cameraPosition,
                this,
                player.transform
            );
        }
    }
}