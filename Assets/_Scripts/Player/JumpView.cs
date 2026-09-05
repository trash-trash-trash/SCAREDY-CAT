using UnityEngine;
using UnityEngine.UI;

public class JumpView : MonoBehaviour
{
    public Slider jumpSlider;

    public PlayerJump jump;

    private bool trackingJump = false;

    void Start()
    {
        jumpSlider.minValue = jump.minJumpPower;
        jumpSlider.maxValue = jump.maxJumpPower;
        jump.AnnounceChargingJump += ShowSetJump;
    }

    void Update()
    {
        if (trackingJump)
        {
            jumpSlider.value = jump.jumpPower;
        }
    }

    private void ShowSetJump(bool obj)
    {
        trackingJump = obj;
        jumpSlider.gameObject.SetActive(obj);
    }

    void OnDestroy()
    {
        jump.AnnounceChargingJump -= ShowSetJump;
    }
}
