using UnityEngine;

public class MovingPlatformCarry : MonoBehaviour
{
    private Vector3 lastPosition;
    private Rigidbody playerRigidbody;

    void Start()
    {
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 movement = transform.position - lastPosition;

        if (playerRigidbody != null)
        {
            playerRigidbody.position += movement;
        }

        lastPosition = transform.position;
    }

    void OnCollisionStay(Collision collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            playerRigidbody = null;
        }
    }
}