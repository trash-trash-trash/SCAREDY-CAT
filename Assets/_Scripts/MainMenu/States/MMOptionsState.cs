using UnityEngine;

public class MMOptionsState : MonoBehaviour
{
    public GameObject optionsObj;

    void OnEnable()
    {
        optionsObj.SetActive(true);
    }

    void OnDisable()
    {
        optionsObj.SetActive(false);
    }
}
