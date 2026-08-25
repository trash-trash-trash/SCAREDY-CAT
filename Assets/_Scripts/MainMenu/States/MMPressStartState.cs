using System;
using UnityEngine;

public class MMPressStartState : MainMenuStateBase
{
    public GameObject pressStartObj;

    public override void OnEnable()
    {
        base.OnEnable();
        pressStartObj.SetActive(true);
    }

    private void OnDisable()
    {
        if (pressStartObj != null)
            pressStartObj.SetActive(false);
    }
}