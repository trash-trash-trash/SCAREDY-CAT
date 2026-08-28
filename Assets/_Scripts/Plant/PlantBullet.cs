using System.Collections;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    public float bulletTime = 5;

    void OnEnable()
    {
        Destroy(gameObject, bulletTime);
    }
}
