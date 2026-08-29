using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    public PlayerBrain playerBrain;

    public Slider lifeSlider;

    public GameObject spriteObj;

    public GameObject canHidePromptObj;
    public TMP_Text canHideText;

    public GameObject investigateTextObj;
    public TMP_Text investigateText;
    
    public GameObject testCanvasObj;
    public TMP_Text playerStateText;

    public SpriteRenderer spriteRenderer;
    private Color originalColor;

    public Animator playerAnimator;
    public AnimationClip idleClip;
    public AnimationClip walkingClip;
    public AnimationClip chargingAttackClip;
    public AnimationClip attackingClip;
    public AnimationClip chargeJumpClip;
    public AnimationClip jumpingClip;
    public AnimationClip fallingClip;
    public AnimationClip climbingClip;
    public AnimationClip takeDamageClip;

    public AnimationClip hideClip;

    public Dictionary<PlayerStates, AnimationClip> animationClipsDict = new Dictionary<PlayerStates, AnimationClip>();

    void Awake()
    {
        originalColor = spriteRenderer.color;
        animationClipsDict.Add(PlayerStates.Idle, idleClip);
        animationClipsDict.Add(PlayerStates.Walking, walkingClip);
        animationClipsDict.Add(PlayerStates.Jumping, jumpingClip);
        animationClipsDict.Add(PlayerStates.ChargingAttack, chargingAttackClip);
        animationClipsDict.Add(PlayerStates.Attacking, attackingClip);
        animationClipsDict.Add(PlayerStates.ChargingJump, chargeJumpClip);
        animationClipsDict.Add(PlayerStates.Falling, fallingClip);
        animationClipsDict.Add(PlayerStates.StickingToWall, climbingClip);
        animationClipsDict.Add(PlayerStates.TakeDamage, takeDamageClip);
        animationClipsDict.Add(PlayerStates.Hiding, hideClip);
        animationClipsDict.Add(PlayerStates.Unhiding, jumpingClip);

        if (playerBrain.testing)
        {
            testCanvasObj.SetActive(true);
        }

        lifeSlider.maxValue = playerBrain.health.maxHealth;

        playerBrain.AnnounceHardFlip += HardFlip;
        playerBrain.AnnouncePlayerState += SetPlayerState;
        playerBrain.AnnounceCanHide += SetCanHidePrompt;
        //playerBrain.AnnounceHidden += FlipHiding;
        playerBrain.health.AnnounceCurrentHealth += SetHealth;
        playerBrain.AnnounceCanInvestigate += SetInvestigateText;
    }

    private void SetInvestigateText(bool input, string newText)
    {
        if (input)
        {
            investigateText.text = newText;
            investigateTextObj.SetActive(true);
        }
        else
        {
            investigateTextObj.SetActive(false);
        }
    }

    private void SetHealth(int obj)
    {
        lifeSlider.value = obj;
    }

    private void FlipHiding(bool IAmHidingNow)
    {
        if (IAmHidingNow)
        {
            Color color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
            spriteRenderer.color = color;
        }
        else
        {
            spriteRenderer.color = originalColor;
        }
    }

    private void SetCanHidePrompt(bool input)
    {
        if (input)
            canHidePromptObj.SetActive(true);
        else
            canHidePromptObj.SetActive(false);
    }

    private void SetPlayerState(PlayerStates newState)
    {
        playerStateText.text = newState.ToString();

        if (animationClipsDict.TryGetValue(newState, out AnimationClip clip))
        {
            playerAnimator.Play(clip.name);

            if (newState == PlayerStates.ClimbingUpLedge)
            {
                if (playerBrain.leftWall)
                    spriteObj.transform.eulerAngles = new Vector3(0, 180, 0);
                else
                    spriteObj.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        //no hiding animation as of yet
        if (newState == PlayerStates.Hiding)
        {
            canHideText.text = "E - EXIT";
        }
        else
        {
            canHideText.text = "E - HIDE";
        }
    }

    //flip sprite according to rb movement, probably not the way to go

    // void Update()
    // {
    //     if (playerBrain.rb.linearVelocity.x > 0.01f)
    //     {
    //         spriteObj.transform.eulerAngles = new Vector3(0, 0, 0);
    //     }
    //     else if (playerBrain.rb.linearVelocity.x < -0.01f)
    //     {
    //         spriteObj.transform.eulerAngles = new Vector3(0, 180, 0);
    //     }
    // }

    //hard flip (for wall jumps)
    public void HardFlip()
    {
        if (spriteObj.transform.eulerAngles.y > 0f)
            spriteObj.transform.eulerAngles = new Vector3(0, 0, 0);
        else
            spriteObj.transform.eulerAngles = new Vector3(0, 180, 0);
    }

    //only flip during said states
    void Update()
    {
        if (playerBrain.currentState == PlayerStates.Walking ||
            playerBrain.currentState == PlayerStates.ChargingAttack ||
            playerBrain.currentState == PlayerStates.ChargingJump)
        {
            spriteObj.transform.eulerAngles = new Vector3(
                0,
                playerBrain.playerMovement.facingDirection == 1f ? 0f : 180f,
                0
            );
        }
    }

    void OnDisable()
    {
        playerBrain.AnnounceHardFlip -= HardFlip;
        playerBrain.AnnounceHidden -= FlipHiding;
        playerBrain.AnnouncePlayerState -= SetPlayerState;
        playerBrain.AnnounceCanHide -= SetCanHidePrompt;
        playerBrain.health.AnnounceCurrentHealth -= SetHealth;
    }
}