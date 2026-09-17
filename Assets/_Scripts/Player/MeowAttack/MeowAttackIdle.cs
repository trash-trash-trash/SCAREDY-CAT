using UnityEngine;
using UnityEngine.InputSystem;

public class MeowAttackIdle : MeowAttackBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        
        attackBrain.inputHandler.AnnounceAttackAction += HandleAttack;
    }
    
    private void HandleAttack(InputAction.CallbackContext context)
    {
        if (!attackBrain.playerAttack.canAttack)
            return;

        if (context.performed)
        {
           attackBrain.ChangeState(MeowAttackState.Charging);
        }
    }
    
    private void OnDisable()
    {
        attackBrain.inputHandler.AnnounceAttackAction -= HandleAttack;
    }
}
