using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 0;

    public bool canTakeDamage = true;

    public bool isAlive = false;

    public event Action AnnounceTakeDamage;
    
    public event Action AnnounceDeath;

    public event Action<int> AnnounceCurrentHealth;

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
        if (amount < 0)
            AnnounceTakeDamage?.Invoke();
        
        int health = currentHealth + amount;
        if (health > maxHealth)
            health = maxHealth;

        else if (health <= 0)
            health = 0;

        currentHealth = health;

        if (currentHealth <= 0)
        {
            isAlive = false;
            AnnounceDeath?.Invoke();
        }
        
        else
            isAlive = true;
        
        AnnounceCurrentHealth?.Invoke(currentHealth);
    }

    public void Hit()
    {
        ChangeHealth(-1);
    }

    public void FlipCanTakeDamage(bool input)
    {
        canTakeDamage = input;
    }
}