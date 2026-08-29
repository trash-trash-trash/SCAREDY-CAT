using UnityEngine;

public class CameraFollowFallback : MonoBehaviour
{
    public Transform player;

    public float smoothSpeed = 5f;

    public float deadZoneWidth = 3f;
    public float deadZoneHeight = 2f;

    public bool fixedCameraActive;

    private float cameraZ;

    void Start()
    {
        cameraZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (fixedCameraActive)
            return;

        Vector3 targetPosition = transform.position;

        float left = transform.position.x - deadZoneWidth / 2f;
        float right = transform.position.x + deadZoneWidth / 2f;

        float bottom = transform.position.y - deadZoneHeight / 2f;
        float top = transform.position.y + deadZoneHeight / 2f;

        if (player.position.x < left)
            targetPosition.x = player.position.x + deadZoneWidth / 2f;

        if (player.position.x > right)
            targetPosition.x = player.position.x - deadZoneWidth / 2f;

        if (player.position.y < bottom)
            targetPosition.y = player.position.y + deadZoneHeight / 2f;

        if (player.position.y > top)
            targetPosition.y = player.position.y - deadZoneHeight / 2f;

        targetPosition.z = cameraZ;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}