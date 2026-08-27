using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DogStates
{
    Idle,
    AggroStand,
    AttackHorizontal,
    AttackJump,
    Defeated
}

public class DogBrain : MonoBehaviour
{
    public DogStates currentState = DogStates.Idle;

    public DogHitbox dogHitbox;
    public Health health;
    
    public Transform pointA;
    public Transform pointB;

    public Vector3 firstPoint;
    public Vector3 secondPoint;

    private GameObject prevObj;

    public ScalePulse scalePulse;

    public GameObject idleObj;
    public GameObject aggroStandObj;
    public GameObject attackHorizontalObj;
    public GameObject attackJumpObj;
    public GameObject defeatedObj;

    public SpriteRenderer spriteRenderer;

    public Dictionary<DogStates, GameObject> statesDict =
        new Dictionary<DogStates, GameObject>();

    public Color originalColor;

    public float damageFlashDuration = 0.7f;
    public float damageFlashInterval = 0.05f;

    public bool defeated = false;
    
    public event Action<DogStates> AnnounceDogState;

    public event Action AnnounceFightStarted;

    private void Awake()
    {
        originalColor = spriteRenderer.color;

        statesDict.Add(DogStates.Idle, idleObj);
        statesDict.Add(DogStates.AggroStand, aggroStandObj);
        statesDict.Add(DogStates.AttackHorizontal, attackHorizontalObj);
        statesDict.Add(DogStates.AttackJump, attackJumpObj);
        statesDict.Add(DogStates.Defeated, defeatedObj);

        health.AnnounceTakeDamage += TakeDamage;
        health.AnnounceDeath += Death;

        ChangeState(DogStates.Idle);
    }

    private void TakeDamage()
    {
        if (defeated)
            return;
        
        health.FlipCanTakeDamage(false);
        StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        Color originalColor = spriteRenderer.color;

        float elapsed = 0f;
        bool isRed = false;

        health.FlipCanTakeDamage(false);

        while (elapsed < damageFlashDuration)
        {
            isRed = !isRed;

            spriteRenderer.color = isRed
                ? Color.red
                : originalColor;

            yield return new WaitForSeconds(damageFlashInterval);

            elapsed += damageFlashInterval;
        }

        spriteRenderer.color = originalColor;
        health.FlipCanTakeDamage(true);
    }

    public void ChangeState(DogStates newState)
    {
        if (statesDict.TryGetValue(newState, out GameObject stateObj))
        {
            if (prevObj != null)
                prevObj.SetActive(false);

            stateObj.SetActive(true);

            prevObj = stateObj;
            currentState = newState;

            AnnounceDogState?.Invoke(currentState);
        }
    }

    public void SwapPoints()
    {
        Vector3 temp = firstPoint;
        firstPoint = secondPoint;
        secondPoint = temp;
    }

    public void StartFight()
    {
        AnnounceFightStarted?.Invoke();
    }

    public void FlipAttacking(bool input)
    {
        dogHitbox.attacking = input;
    }

    private void Death()
    {
        ChangeState(DogStates.Defeated);
    }

    private void OnDestroy()
    {
        health.AnnounceTakeDamage -= TakeDamage;
        health.AnnounceDeath -= Death;
    }
}