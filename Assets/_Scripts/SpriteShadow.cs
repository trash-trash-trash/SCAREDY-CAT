using UnityEngine;

public class SpriteShadow : MonoBehaviour
{
    public SpriteRenderer catSprite;
    public SpriteRenderer shadowSprite;

    void LateUpdate()
    {
        shadowSprite.sprite = catSprite.sprite;
        shadowSprite.flipX = catSprite.flipX;
        shadowSprite.flipY = catSprite.flipY;
    }
}
