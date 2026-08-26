using System.Collections;
using UnityEngine;

public class PSTakeDamage  : PlayerStateBase
{
    public float takeDamageTime = 0.3f;

    public float knockBackVertForce = 5;
    public float knockBackHorForce = 5;
    
    public override void OnEnable()
    {
        base.OnEnable();
        
        //knock back
        playerBrain.rb.AddForce(new Vector3(knockBackHorForce, knockBackVertForce, 0));
        
        StartCoroutine(TakeDamageCoro());
    }

    IEnumerator TakeDamageCoro()
    {
        yield return new WaitForSeconds(takeDamageTime);
        if(playerBrain.groundCheck.targetLayerDetected)
            playerBrain.ChangeState(PlayerStates.Idle);
        else
            playerBrain.ChangeState(PlayerStates.Falling);
    }
}
