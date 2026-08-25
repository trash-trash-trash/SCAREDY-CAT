using UnityEngine;

public class MainMenuStateBase : MonoBehaviour
{
    public MainMenu mainMenu;

    public virtual void OnEnable()
    {
        mainMenu = GetComponentInParent<MainMenu>();
    }
}