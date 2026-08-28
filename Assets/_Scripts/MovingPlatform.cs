using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  
    public Transform pointA;
    public Transform pointB;
    public float speed;

    public bool moving_To_B = true;

    void Update()
    {
        if (moving_To_B)
        {
            transform.position = Vector3.MoveTowards (transform.position, pointB.position, speed * Time.deltaTime );

            if (Vector3.Distance(transform.position, pointB.position) < 0.01f)
            {
                moving_To_B = false;
            }
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, pointA.position) < 0.01f)
            {
                moving_To_B = true;
            }
        }
    }
}

