using UnityEngine;

public class CameraRoomTrigger : MonoBehaviour
{
    public Transform cameraPosition;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            CameraController.Instance.MoveToRoom(cameraPosition);
        }
    }
}
