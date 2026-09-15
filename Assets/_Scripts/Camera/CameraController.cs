using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private float moveSpeed = 5f;

    public Transform target;

    private Transform trackedPlayer;
    private CameraRoomTrigger currentRoom;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.position;

        if (currentRoom != null && currentRoom.isTracker && trackedPlayer != null)
        {
            if (currentRoom.trackAxis == CameraRoomTrigger.TrackAxis.LeftRight)
                targetPosition.x = trackedPlayer.position.x;

            if (currentRoom.trackAxis == CameraRoomTrigger.TrackAxis.UpDown)
                targetPosition.y = trackedPlayer.position.y;
        }

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    public void MoveToRoom(Transform roomPosition, CameraRoomTrigger room, Transform player)
    {
        target = roomPosition;
        currentRoom = room;
        trackedPlayer = player;
    }
}