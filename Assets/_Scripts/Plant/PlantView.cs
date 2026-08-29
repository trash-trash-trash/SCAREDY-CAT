using UnityEngine;

public class PlantView : MonoBehaviour
{
   public PlantBrain plantBrain;
   
   public Sprite defaultSprite;
   public Sprite defeatedSprite;
   
   public SpriteRenderer spriteRenderer;

   void Awake()
   {
      plantBrain.AnnouncePlantState += SetSprite;
   }

   private void SetSprite(PlantStates newState)
   {
        if (newState == PlantStates.Defeated)
     
            spriteRenderer.sprite = defeatedSprite;

        else
            spriteRenderer.sprite = defaultSprite;
   }

   void OnDisable()
   {
      plantBrain.AnnouncePlantState -= SetSprite;
   }
}
