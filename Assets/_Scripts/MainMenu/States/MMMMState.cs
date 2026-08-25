using System;
using UnityEngine;

public class MMMMState : MainMenuStateBase
{
    public GameObject mainMenuObj;

    public override void OnEnable()
    {
        base.OnEnable();
        mainMenuObj.SetActive(true);
    }

    private void OnDisable()
    {
        if (mainMenuObj != null)
            mainMenuObj.SetActive(false);
    }
}