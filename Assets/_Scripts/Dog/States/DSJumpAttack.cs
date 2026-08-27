using System.Collections;
using UnityEngine;

public class DSJumpAttack : DogStateBase
{
    [SerializeField] private float jumpDuration = 0.8f;
    [SerializeField] private float jumpHeight = 3f;

    public override void OnEnable()
    {
        base.OnEnable();
        dogBrain.FlipAttacking(true);
        StartCoroutine(LeapToSecondPoint());
    }

    private IEnumerator LeapToSecondPoint()
    {
        Vector3 start = dogBrain.transform.position;
        Vector3 target = dogBrain.secondPoint;

        float elapsed = 0f;

        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / jumpDuration;

            Vector3 position = Vector3.Lerp(
                start,
                target,
                t
            );

            float arc = Mathf.Sin(t * Mathf.PI) * jumpHeight;

            position.y += arc;

            dogBrain.transform.position = position;

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
