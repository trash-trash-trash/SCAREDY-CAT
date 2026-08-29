using UnityEngine;

public class FixedCameraZone : MonoBehaviour
{
    private CameraFollowFallback cameraFollow;
    private CameraController cameraController;

    void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollowFallback>();
        cameraController = CameraController.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            cameraFollow.fixedCameraActive = true;
            cameraController.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            cameraFollow.fixedCameraActive = false;
            cameraController.enabled = false;
        }
    }
}