using UnityEngine;

public class DogHitbox : MonoBehaviour
{
    public int attackDamage = -34;

    public bool attacking = false;

    private void OnTriggerStay(Collider other)
    {
        if (!attacking)
            return;

        PlayerBrain playerBrain = other.GetComponent<PlayerBrain>();

        if (playerBrain == null)
            return;

        if (playerBrain.health.canTakeDamage)
            playerBrain.health.ChangeHealth(attackDamage);
    }
}