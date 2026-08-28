using System.Collections;
using UnityEngine;

public class PSAttacking : PlantStateBase
{
    public float attackTime = 3f;
    
    public float bulletSpeed = 10f;

    public override void OnEnable()
    {
        base.OnEnable();

        GameObject go = Instantiate(
            plantBrain.plantBullet,
            transform.position,
            transform.rotation,
            plantBrain.transform
        );

        Vector3 localPlayerPosition =
            plantBrain.transform.InverseTransformPoint(plantBrain.playerTransform.position);

        Vector3 localBulletPosition =
            plantBrain.transform.InverseTransformPoint(transform.position);

        Vector3 direction = (localPlayerPosition - localBulletPosition).normalized;

        Rigidbody rb = go.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * bulletSpeed;
        }

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(attackTime);
        plantBrain.ChangeState(PlantStates.AggroStand);
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}