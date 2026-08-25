using UnityEngine;

public class MMOptionsState : MainMenuStateBase
{
    public GameObject optionsObj;

    public override void OnEnable()
    {
        base.OnEnable();
        optionsObj.SetActive(true);
    }

    void OnDisable()
    {
        if(optionsObj!=null)
            optionsObj.SetActive(false);
    }
}
