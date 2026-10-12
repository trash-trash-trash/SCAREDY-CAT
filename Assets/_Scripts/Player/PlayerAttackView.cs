using UnityEngine;

public class PlayerAttackView : MonoBehaviour
{
    public PlayerAttack attack;
    public GameObject attackRadiusPreview;
    public GameObject meowSpriteGameObject;

    [Header("Meow Attack Scaling")] [SerializeField]
    private float minRadius = 3f;

    [SerializeField] private float minScaleMultiplier = 0.5f;
    [SerializeField] private float maxScaleMultiplier = 3f;

    private bool trackingAttackCharge = false;

    private float currentAttackRadius;
    private Vector3 originalMeowScale;
    
    private Coroutine meowCoroutine;

    private void Start()
    {
        originalMeowScale = meowSpriteGameObject.transform.localScale;

        attack.AnnounceChargingAttack += ShowAttack;
    }

    private void Update()
    {
        if (trackingAttackCharge)
            UpdateAttackRadius();
    }

    private void UpdateAttackRadius()
    {
        float chargePercent = Mathf.Clamp01(
            attack.chargeTimer / attack.chargeTime
        );

        currentAttackRadius = Mathf.Lerp(
            minRadius,
            attack.maxAttackRadius,
            chargePercent
        );

        float diameter = currentAttackRadius * 2f;

        attackRadiusPreview.transform.localScale =
            Vector3.one * diameter;

        attackRadiusPreview.transform.position =
            attack.transform.position;
    }

    private void ShowAttack(bool isCharging)
    {
        trackingAttackCharge = isCharging;

        if (isCharging)
        {
            if (!attackRadiusPreview.activeSelf)
                attackRadiusPreview.SetActive(true);

            UpdateAttackRadius();
        }
        else
        {
            // Hide the radius preview
            if (attackRadiusPreview.activeSelf)
                attackRadiusPreview.SetActive(false);

            // Show the meow at the size reached when charging ended
            ShowMeowAttack();
        }
    }

    private void ShowMeowAttack()
    {
        float radiusPercent = Mathf.InverseLerp(
            minRadius,
            attack.maxAttackRadius,
            currentAttackRadius
        );

        float scaleMultiplier = Mathf.Lerp(
            minScaleMultiplier,
            maxScaleMultiplier,
            radiusPercent
        );

        meowSpriteGameObject.transform.localScale =
            originalMeowScale * scaleMultiplier;

        if (!meowSpriteGameObject.activeSelf)
            meowSpriteGameObject.SetActive(true);

        if (meowCoroutine != null)
            StopCoroutine(meowCoroutine);

        meowCoroutine = StartCoroutine(HideMeowAfterDelay());
    }

    private System.Collections.IEnumerator HideMeowAfterDelay()
    {
        yield return new WaitForSeconds(attack.attackDuration);

        meowSpriteGameObject.SetActive(false);
        meowSpriteGameObject.transform.localScale = originalMeowScale;

        meowCoroutine = null;
    }

    private void OnDestroy()
    {
        attack.AnnounceChargingAttack -= ShowAttack;
    }
}