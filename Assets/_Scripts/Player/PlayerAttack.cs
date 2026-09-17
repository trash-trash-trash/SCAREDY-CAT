using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float minAttackPower = 5f;
    [SerializeField] private float maxAttackPower = 20f;
    [SerializeField] private float chargeTime = 1f;

    private float minAttackRadius = 3f;

    [SerializeField] private float maxAttackRadius = 7f;

    [SerializeField] private int attackDamage = -100;

    [SerializeField] private LayerMask attackLayers;
    [SerializeField] private int maxTargets = 32;

    public GameObject attackVisual;
    public float attackVisualTime = 0.3f;
    public PlayerInputs inputHandler;
    public PlayerMovement playerMovement;

    public bool canAttack = true;

    private Vector2 moveInput;

    public bool chargingAttack;
    public float chargeTimer;
    public float attackPower;

    [SerializeField] private float attackDuration = 0.3f;

    private HashSet<Health> hitTargets = new HashSet<Health>();
    private Coroutine attackCoroutine;

    private Collider[] attackResults;

    private Coroutine attackVisualCoroutine;

    // true = charging, false = released
    public event Action<bool> AnnounceChargingAttack;

    private void Awake()
    {
        attackResults = new Collider[maxTargets];
    }

    private void Update()
    {
        moveInput = inputHandler.moveInput;

        if (chargingAttack)
        {
            chargeTimer += Time.deltaTime;

            float chargePercent = Mathf.Clamp01(
                chargeTimer / chargeTime
            );

            attackPower = Mathf.Lerp(
                minAttackPower,
                maxAttackPower,
                chargePercent
            );
        }
    }

    public void FlipCanAttack(bool input)
    {
        canAttack = input;
    }

    public void StartCharging()
    {
        chargingAttack = true;
        chargeTimer = 0f;
        attackPower = minAttackPower;

        AnnounceChargingAttack?.Invoke(true);
    }

    public void ReleaseAttack()
    {
        if (!chargingAttack)
            return;

        chargingAttack = false;

        float chargePercent = Mathf.Clamp01(
            chargeTimer / chargeTime
        );

        attackPower = Mathf.Lerp(
            minAttackPower,
            maxAttackPower,
            chargePercent
        );

        PerformAttack(attackPower);

        chargeTimer = 0f;
        attackPower = minAttackPower;

        AnnounceChargingAttack?.Invoke(false);
    }


    private void PerformAttack(float power)
    {
        float chargePercent = Mathf.InverseLerp(
            minAttackPower,
            maxAttackPower,
            power
        );

        float attackRadius = Mathf.Lerp(
            minAttackRadius,
            maxAttackRadius,
            chargePercent
        );

        // Reset targets for this new attack
        hitTargets.Clear();

        // Start the attack over time
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        attackCoroutine = StartCoroutine(
            PerformAttackCoroutine(attackRadius)
        );

        ShowAttackVisual();
    }

    private IEnumerator PerformAttackCoroutine(float attackRadius)
    {
        float timer = 0f;

        while (timer < attackDuration)
        {
            // Always use the player's current position
            Vector3 attackCenter = transform.position;

            int hitCount = Physics.OverlapSphereNonAlloc(
                attackCenter,
                attackRadius,
                attackResults,
                attackLayers
            );

            for (int i = 0; i < hitCount; i++)
            {
                Collider hit = attackResults[i];

                Health health = hit.GetComponent<Health>();

                if (health == null)
                    continue;

                // Only damage each target once during this attack
                if (hitTargets.Contains(health))
                    continue;

                hitTargets.Add(health);

                health.ChangeHealth(attackDamage);
            }

            timer += Time.deltaTime;

            yield return null;
        }

        attackCoroutine = null;
        hitTargets.Clear();
    }


    private void ShowAttackVisual()
    {
        if (attackVisualCoroutine != null)
            StopCoroutine(attackVisualCoroutine);

        attackVisualCoroutine = StartCoroutine(
            ShowAttackVisualCoroutine()
        );
    }


    private IEnumerator ShowAttackVisualCoroutine()
    {
        attackVisual.SetActive(true);

        float timer = 0f;

        while (timer < attackVisualTime)
        {
            // Keep the visual centered on the player
            attackVisual.transform.position = transform.position;

            timer += Time.deltaTime;

            yield return null;
        }

        attackVisual.SetActive(false);
        attackVisualCoroutine = null;
    }
}