using UnityEngine;

public class PlayerStateBase : MonoBehaviour
{
    public PlayerBrain playerBrain;

    public virtual void OnEnable()
    {
        playerBrain = GetComponentInParent<PlayerBrain>();
    }
}
