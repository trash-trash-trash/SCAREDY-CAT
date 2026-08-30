using System;
using UnityEngine;

public class MMMMState : MainMenuStateBase
{
    public GameObject mainMenuObj;

    public GameObject creditsObj;

    public override void OnEnable()
    {
        base.OnEnable();
        
        if(mainMenu.finishedGame)
            creditsObj.SetActive(true);
        else
            creditsObj.SetActive(false);
        
        mainMenuObj.SetActive(true);
    }

    private void OnDisable()
    {
        if (mainMenuObj != null)
            mainMenuObj.SetActive(false);
    }
}