using UnityEngine;

public class MMInGameState : MainMenuStateBase
{
    public GameObject mainMenuObj;

    public override void OnEnable()
    {
        base.OnEnable();
        mainMenuObj.SetActive(false);
    }

    void OnDisable()
    {
        if(mainMenuObj != null)
            mainMenuObj.SetActive(true);
    }
}
