using UnityEngine;

public class CameraRoomTrigger : MonoBehaviour
{
    public Transform cameraPosition;
    
    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            if(CameraController.Instance.target!= cameraPosition)
                CameraController.Instance.MoveToRoom(cameraPosition);
        }
    }
}
