using UnityEngine;

public class MovingPlatformParent : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
       
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
           
            collision.transform.SetParent(null);
        }
    }
}
