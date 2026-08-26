using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private float moveSpeed = 5f;

    public Transform target;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (target == null)
            return;

        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );
    }

    public void MoveToRoom(Transform roomPosition)
    {
        target = roomPosition;
    }
}