using UnityEngine;
using UnityEngine.InputSystem;

public class TestAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Material material;

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;

            material.SetFloat("_FlipX", spriteRenderer.flipX ? 1f : 0f);

            Debug.Log("Sprite flip: " + spriteRenderer.flipX);
            Debug.Log("Shader value: " + material.GetFloat("_FlipX"));
        }
    }
}