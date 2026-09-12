using UnityEngine;

public class PlayerStateBase : MonoBehaviour
{
    public PlayerBrain playerBrain;

    public virtual void OnEnable()
    {
        playerBrain = GetComponentInParent<PlayerBrain>();
    }

    public void FlipLookingForWallChecks(bool input)
    {
        playerBrain.lookingForWallChecks = input;
        playerBrain.leftWallCheck.FlipLookingForTarget(input);
        playerBrain.rightWallCheck.FlipLookingForTarget(input);
    }
}
