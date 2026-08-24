using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerBrain playerBrain;
    
    public GameObject testCanvasObj;
    public TMP_Text playerStateText;

    void Awake()
    {
        if (playerBrain.testing)
        {
            testCanvasObj.SetActive(true);

            playerBrain.AnnouncePlayerState += SetPlayerStateText;
        }
    }

    private void SetPlayerStateText(PlayerStates obj)
    {
        playerStateText.text = obj.ToString();
    }
}
