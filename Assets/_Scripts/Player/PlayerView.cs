using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerBrain playerBrain;

    public GameObject spriteObj;
    
    public GameObject testCanvasObj;
    public TMP_Text playerStateText;
    
    public Animator playerAnimator;
    public AnimationClip idleClip;
    public AnimationClip walkingClip;
    public AnimationClip chargeJumpClip;
    public AnimationClip jumpingClip;
    public AnimationClip fallingClip;
    
    public Dictionary<PlayerStates, AnimationClip> animationClipsDict =  new Dictionary<PlayerStates, AnimationClip>();
    void Awake()
    {
        animationClipsDict.Add(PlayerStates.Idle, idleClip);
        animationClipsDict.Add(PlayerStates.Walking, walkingClip);
        animationClipsDict.Add(PlayerStates.Jumping, jumpingClip);
        animationClipsDict.Add(PlayerStates.ChargingJump,  chargeJumpClip);
        animationClipsDict.Add(PlayerStates.Falling, fallingClip);
        
        if (playerBrain.testing)
        {
            testCanvasObj.SetActive(true);

        }
        
        playerBrain.AnnouncePlayerState += SetPlayerState;
    }

    private void SetPlayerState(PlayerStates newState)
    {
        playerStateText.text = newState.ToString();
        
        if(animationClipsDict.TryGetValue(newState, out AnimationClip clip))
        {
            playerAnimator.Play(clip.name);
        }
    }
    
    //flip sprite according to rb movement

    void Update()
    {
        if (playerBrain.rb.linearVelocity.x > 0.01f)
        {
            spriteObj.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (playerBrain.rb.linearVelocity.x < -0.01f)
        {
            spriteObj.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void OnDisable()
    {
        playerBrain.AnnouncePlayerState -= SetPlayerState;
    }
}
