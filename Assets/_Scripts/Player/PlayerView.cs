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

    public GameObject playerSpriteAnchor;

    public Vector3 anchorPos;

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

        //reusing sprites
        animationClipsDict.Add(PlayerStates.StickingToRoof, climbingClip);
        animationClipsDict.Add(PlayerStates.ChargingRoofJump, chargeJumpClip);

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
        anchorPos = playerSpriteAnchor.transform.localPosition;
        playerBrain.AnnounceFlipSprite180 += FlipSprite180;
    }

    private void FlipSprite180()
    {
        Vector3 rotation = spriteObj.transform.eulerAngles;

        rotation.y = rotation.y == 0f ? 180f : 0f;

        spriteObj.transform.eulerAngles = rotation;
    }

    void OnEnable()
    {
        lifeSlider.value = playerBrain.health.currentHealth;
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

            // if (newState == PlayerStates.ClimbingUpLedge)
            // {
            //     if (playerBrain.leftWall)
            //         spriteObj.transform.eulerAngles = new Vector3(0, 180, 0);
            //     else
            //         spriteObj.transform.eulerAngles = new Vector3(0, 0, 0);
            // }

            if (newState == PlayerStates.ChargingWallJump || newState == PlayerStates.StickingToWall)
            {
                if (playerBrain.leftWall)
                {
                    playerSpriteAnchor.transform.localPosition = new Vector3(
                        playerSpriteAnchor.transform.localPosition.x + 0.54f,
                        playerSpriteAnchor.transform.localPosition.y, playerSpriteAnchor.transform.localPosition.z);
                }
            }
            else
            {
                playerSpriteAnchor.transform.localPosition = anchorPos;
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
        playerBrain.playerMovement.facingDirection *= -1f;
    }

    void Update()
    {
        // These states should NOT update the sprite's facing direction.
        // Whatever Y rotation the sprite currently has should be preserved.
        //
        // We DO reset Z to 0 because these states should not remain
        // visually rotated onto the roof.
        bool preserveFacing =
            playerBrain.currentState == PlayerStates.ClimbingUpLedge ||
            playerBrain.currentState == PlayerStates.Jumping ||
            playerBrain.currentState == PlayerStates.TakeDamage ||
            playerBrain.currentState == PlayerStates.Falling;

        if (preserveFacing)
        {
            Vector3 rotation = spriteObj.transform.eulerAngles;

            // Reset the roof rotation, but leave Y (facing) untouched.
            rotation.z = 0f;

            spriteObj.transform.eulerAngles = rotation;
            return;
        }


        // Sticking to the roof uses the normal facing direction,
        // but inverted because the player is upside down.
        if (playerBrain.currentState == PlayerStates.StickingToRoof)
        {
            float yRotation =
                playerBrain.playerMovement.facingDirection == 1f
                    ? 180f
                    : 0f;

            spriteObj.transform.eulerAngles = new Vector3(
                0f,
                yRotation,
                90f
            );

            return;
        }


        // Charging a roof jump uses the inverted facing direction,
// with an additional 180° rotation on the Z axis.
        if (playerBrain.currentState == PlayerStates.ChargingRoofJump)
        {
            float yRotation =
                playerBrain.playerMovement.facingDirection == 1f
                    ? 180f
                    : 0f;

            spriteObj.transform.eulerAngles = new Vector3(
                0f,
                yRotation,
                180f
            );

            return;
        }


        // All other normal states use the player's facing direction.
        float normalYRotation =
            playerBrain.playerMovement.facingDirection == 1f
                ? 0f
                : 180f;

        spriteObj.transform.eulerAngles = new Vector3(
            0f,
            normalYRotation,
            0f
        );
    }

    void OnDisable()
    {
        playerBrain.AnnounceHardFlip -= HardFlip;
        playerBrain.AnnounceHidden -= FlipHiding;
        playerBrain.AnnouncePlayerState -= SetPlayerState;
        playerBrain.AnnounceCanHide -= SetCanHidePrompt;
        playerBrain.health.AnnounceCurrentHealth -= SetHealth;
        playerBrain.AnnounceFlipSprite180 -= FlipSprite180;
    }
}