using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float minAttackPower = 5f;
    [SerializeField] private float maxAttackPower = 20f;
    [SerializeField] private float chargeTime = 1f;

    private float minAttackRadius = 1f;

    [SerializeField] private float maxAttackRadius = 4f;
    [SerializeField] private float attackOffset = 1f;

    [SerializeField] private int attackDamage = -100;

    [SerializeField] private LayerMask attackLayers;
    [SerializeField] private int maxTargets = 32;

    public GameObject attackVisual;
    public float attackVisualTime = 0.3f;
    public PlayerInputs inputHandler;
    public PlayerMovement playerMovement;

    public float lastAttackDirection = 1f;
    public bool canAttack = true;

    private Vector2 moveInput;

    public bool chargingAttack;
    public float chargeTimer;
    public float attackPower;

    private Collider[] attackResults;

    private Coroutine attackVisualCoroutine;

    // true = charging, false = released
    public event Action<bool> AnnounceChargingAttack;

    private void Awake()
    {
        attackResults = new Collider[maxTargets];
    }

    private void OnEnable()
    {
        inputHandler.AnnounceAttackAction += HandleAttack;
    }

    private void Update()
    {
        moveInput = inputHandler.moveInput;

        if (Mathf.Abs(moveInput.x) > 0.01f)
        {
            lastAttackDirection = Mathf.Sign(playerMovement.facingDirection);
        }

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

    private void HandleAttack(InputAction.CallbackContext context)
    {
        if (!canAttack)
            return;

        if (context.performed)
        {
            StartCharging();
        }

        if (context.canceled)
        {
            ReleaseAttack();
        }
    }

    private void StartCharging()
    {
        chargingAttack = true;
        chargeTimer = 0f;
        attackPower = minAttackPower;

        AnnounceChargingAttack?.Invoke(true);
    }

    private void ReleaseAttack()
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

        Vector3 attackDirection = Vector3.right * lastAttackDirection;

        PerformAttack(attackDirection, attackPower);

        chargeTimer = 0f;
        attackPower = minAttackPower;

        AnnounceChargingAttack?.Invoke(false);
    }

    private void PerformAttack(Vector3 direction, float power)
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

        Vector3 attackCenter = transform.position
                               + direction * attackOffset;

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

            health.ChangeHealth(attackDamage);
            Debug.Log("Changed " + hit.name + " health by " + attackDamage);
        }
        ShowAttackVisual(attackCenter);

        Debug.Log(
            $"Attack! Direction: {direction}, Power: {power}, Radius: {attackRadius}, Hits: {hitCount}"
        );
    }

    private void ShowAttackVisual(Vector3 position)
    {
        if (attackVisualCoroutine != null)
            StopCoroutine(attackVisualCoroutine);

        attackVisualCoroutine = StartCoroutine(
            ShowAttackVisualCoroutine(position)
        );
    }

    private IEnumerator ShowAttackVisualCoroutine(Vector3 position)
    {
        attackVisual.transform.position = position;
        attackVisual.SetActive(true);

        yield return new WaitForSeconds(attackVisualTime);

        attackVisual.SetActive(false);
        attackVisualCoroutine = null;
    }


    private void OnDisable()
    {
        inputHandler.AnnounceAttackAction -= HandleAttack;
    }
}