using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 0;

    public bool canTakeDamage = true;

    public bool isAlive = false;

    void Awake()
    {
        Res();
    }

    public void Res()
    {
        ChangeHealth(maxHealth);
    }

    public void ChangeHealth(int amount)
    {
        int health = currentHealth + amount;
        if (health > maxHealth)
            health = maxHealth;

        else if (health <= 0)
            health = 0;

        currentHealth = health;

        if (currentHealth <= 0)
            isAlive = false;
        else
            isAlive = true;
    }
}