using UnityEngine;

public class RotateTransform : MonoBehaviour
{
    public float spinSpeed = 5f;
    
    public Vector3 spinDirection = Vector3.up;

    public void Update()
    {
        transform.Rotate(spinDirection * spinSpeed * Time.deltaTime);
    }
}
