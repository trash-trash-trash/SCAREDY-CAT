using System.Collections;
using UnityEngine;

public class DSAggroStand : DogStateBase
{
    public float waitTime;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;

    public bool jumpAttack = false;
    
    public override void OnEnable()
    {
        base.OnEnable();
        
        //face Dog in the correct direction
        if (dogBrain.secondPoint.x < dogBrain.firstPoint.x)
            dogBrain.spriteRenderer.flipX = false;
        else
            dogBrain.spriteRenderer.flipX = true;
        
        dogBrain.scalePulse.StartPulse();
        waitTime = UnityEngine.Random.Range(
            minWaitTime,
            maxWaitTime
        );
        if (Random.value < 0.5f)
            jumpAttack = false;
        else
            jumpAttack = true;

        StartCoroutine(WaitThenAttack(waitTime));
    }

    IEnumerator WaitThenAttack(float time)
    {
        yield return new WaitForSeconds(time);
        
        if(jumpAttack)
            dogBrain.ChangeState(DogStates.AttackJump);
        else
            dogBrain.ChangeState(DogStates.AttackHorizontal);
    }

    void OnDisable()
    {
        dogBrain.scalePulse.EndPulse();
    }
}
