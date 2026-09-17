using UnityEngine;
using UnityEngine.InputSystem;

public class MeowAttackCharge : MeowAttackBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        attackBrain.playerAttack.StartCharging();
        attackBrain.inputHandler.AnnounceAttackAction += HandleAttack;
    }
    
    private void HandleAttack(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            attackBrain.ChangeState(MeowAttackState.Attacking);
        }
    }
    
    private void OnDisable()
    {
        attackBrain.inputHandler.AnnounceAttackAction -= HandleAttack;
    }
}
