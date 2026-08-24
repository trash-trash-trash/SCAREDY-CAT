using System;
using System.Collections.Generic;
using UnityEngine;

public enum MainMenuStates
{
    InGame,
    PressStart,
    MainMenu,
    Options,
    Quit
}

public class MainMenu : MonoBehaviour
{
    public MainMenuStates currentState = MainMenuStates.PressStart;

    private GameObject prevObj;
    public GameObject inGameObj;
    public GameObject pressStartObj;
    public GameObject mainMenuObj;
    public GameObject optionsObj;
    public GameObject QuitObj;

    public Dictionary<MainMenuStates, GameObject> statesDict = new Dictionary<MainMenuStates, GameObject>();

    public event Action<MainMenuStates> AnnounceMainMenuState;

    void OnEnable()
    {
        statesDict.Add(MainMenuStates.InGame, inGameObj);
        statesDict.Add(MainMenuStates.PressStart, pressStartObj);
        statesDict.Add(MainMenuStates.MainMenu, mainMenuObj);
        statesDict.Add(MainMenuStates.Options, optionsObj);
        statesDict.Add(MainMenuStates.Quit, QuitObj);

        ChangeState(MainMenuStates.PressStart);
    }

    public void ChangeState(MainMenuStates newState)
    {
        if (statesDict.TryGetValue(newState, out GameObject stateObj))
        {
            if (prevObj != null)
                prevObj.SetActive(false);

            stateObj.SetActive(true);
            prevObj = stateObj;
            currentState = newState;

            AnnounceMainMenuState?.Invoke(currentState);
        }
    }

    public void PressStart()
    {
        ChangeState(MainMenuStates.MainMenu);
    }

    public void StartGame()
    {
        ChangeState(MainMenuStates.InGame);
    }

    public void Options()
    {
        ChangeState(MainMenuStates.Options);
    }

    public void Meow()
    {
        Debug.Log("MEOW!!!");
    }

    public void Quit()
    {
        ChangeState(MainMenuStates.Quit);
    }

    public void ReallyQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    void OnDisable()
    {
    }
}