using UnityEngine;

public class MMInGameState : MonoBehaviour
{
    public GameObject mainMenuObj;

    void OnEnable()
    {
        mainMenuObj.SetActive(false);
    }

    void OnDisable()
    {
        if(mainMenuObj != null)
            mainMenuObj.SetActive(true);
    }
}
