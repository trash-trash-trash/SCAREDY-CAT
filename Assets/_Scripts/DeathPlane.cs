using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public int attackDamage = -666;

    private void OnTriggerEnter(Collider other)
    {
        PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

        if (playerBrain == null)
            return;

        if (playerBrain.health.canTakeDamage)
            playerBrain.health.ChangeHealth(attackDamage);
    }
}