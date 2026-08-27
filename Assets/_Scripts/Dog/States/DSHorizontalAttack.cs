using System.Collections;
using UnityEngine;

public class DSHorizontalAttack : DogStateBase
{
    [SerializeField] private float horizontalAttackDuration = 0.5f;

    public override void OnEnable()
    {
        base.OnEnable();
        dogBrain.FlipAttacking(true);
        StartCoroutine(MoveToSecondPoint());
    }

    private IEnumerator MoveToSecondPoint()
    {
        Vector3 start = dogBrain.transform.position;
        Vector3 target = dogBrain.secondPoint;

        float elapsed = 0f;

        while (elapsed < horizontalAttackDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / horizontalAttackDuration;

            dogBrain.transform.position = Vector3.Lerp(
                start,
                target,
                t
            );

            yield return null;
        }

        dogBrain.transform.position = target;

        dogBrain.SwapPoints();

        dogBrain.ChangeState(DogStates.AggroStand);
    }

    void OnDisable()
    {
        dogBrain.FlipAttacking(false);
    }
}
