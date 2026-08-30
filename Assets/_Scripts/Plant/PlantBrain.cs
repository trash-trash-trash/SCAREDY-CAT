using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantStates
{
    Idle,
    AggroStand,
    Attacking,
    Defeated
}

public class PlantBrain : MonoBehaviour
{
    public PlantStates currentState = PlantStates.Idle;

    public Transform plantView;

    public PlantFightTrigger plantFightTrigger;
    public Transform playerTransform;

    public GameObject plantBullet;

    public Health health;

    public SpriteRenderer spriteRenderer;

    private GameObject prevObj;

    public GameObject idleObj;
    public GameObject aggroStandObj;
    public GameObject attackingObj;
    public GameObject defeatedObj;

    public Dictionary<PlantStates, GameObject> statesDict =
        new Dictionary<PlantStates, GameObject>();

    public float damageFlashDuration = 1.2f;
    public float damageFlashInterval = 0.05f;
    public float invincibleDuration = 1.5f;

    public InvestigateSpot investigateSpot;

    public event Action<PlantStates> AnnouncePlantState;

    private Color originalColor;

    void Awake()
    {
        originalColor = spriteRenderer.color;
        statesDict.Add(PlantStates.Idle, idleObj);
        statesDict.Add(PlantStates.AggroStand, aggroStandObj);
        statesDict.Add(PlantStates.Attacking, attackingObj);
        statesDict.Add(PlantStates.Defeated, defeatedObj);

        ChangeState(PlantStates.Idle);
        health.AnnounceTakeDamage += TakeDamage;
        health.AnnounceDeath += Die;

        plantFightTrigger.AnnouncePlayerDetected += SetTarget;
        plantFightTrigger.AnnouncePlayerLost += PlayerLost;
    }

    private void SetTarget(Transform target)
    {
        if(currentState==PlantStates.Idle)
        {
            playerTransform = target;
            ChangeState(PlantStates.AggroStand);
        }
    }

    private void PlayerLost()
    {
        if (health.isAlive)
            ChangeState(PlantStates.Idle);
    }

    private void TakeDamage()
    {
        if (!health.canTakeDamage)
            return;
        StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
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

        yield return new WaitForSeconds(invincibleDuration);

        health.FlipCanTakeDamage(true);
    }

    private void Die()
    {
        if (currentState == PlantStates.Defeated)
            return;

        spriteRenderer.color = originalColor;
        ChangeState(PlantStates.Defeated);
        plantView.transform.position += new Vector3(0, -3f, 0);
        health.FlipCanTakeDamage(false);
    }

    public void ChangeState(PlantStates newState)
    {
        if (statesDict.TryGetValue(newState, out GameObject stateObj))
        {
            if (prevObj != null)
                prevObj.SetActive(false);

            stateObj.SetActive(true);

            prevObj = stateObj;
            currentState = newState;

            AnnouncePlantState?.Invoke(currentState);
        }
    }

    private void OnDisable()
    {
        health.AnnounceTakeDamage -= TakeDamage;
        health.AnnounceDeath -= Die;
        plantFightTrigger.AnnouncePlayerDetected -= SetTarget;
        plantFightTrigger.AnnouncePlayerLost -= PlayerLost;
    }
}