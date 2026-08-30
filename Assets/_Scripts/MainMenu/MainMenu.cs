using System;
using System.Collections.Generic;
using UnityEngine;

public enum MainMenuStates
{
    InGame,
    PressStart,
    MainMenu,
    Options,
    Quit,
    EndGame
}

public class MainMenu : MonoBehaviour
{
    public MainMenuStates currentState = MainMenuStates.PressStart;

    private GameObject prevObj;
    public GameObject inGameObj;
    public GameObject pressStartObj;
    public GameObject mainMenuObj;
    public GameObject optionsObj;
    public GameObject quitObj;
    public GameObject endGameObj;

    public Dictionary<MainMenuStates, GameObject> statesDict = new Dictionary<MainMenuStates, GameObject>();

    public event Action<MainMenuStates> AnnounceMainMenuState;

    public bool testingGameplay = false;

    void OnEnable()
    {
        statesDict.Add(MainMenuStates.InGame, inGameObj);
        statesDict.Add(MainMenuStates.PressStart, pressStartObj);
        statesDict.Add(MainMenuStates.MainMenu, mainMenuObj);
        statesDict.Add(MainMenuStates.Options, optionsObj);
        statesDict.Add(MainMenuStates.Quit, quitObj);
        statesDict.Add(MainMenuStates.EndGame, endGameObj);

        if(testingGameplay)
            ChangeState(MainMenuStates.InGame);
        
        else
            ChangeState(MainMenuStates.PressStart);
    }

    public void ChangeState(MainMenuStates newState)
    {
        if (statesDict.TryGetValue(newState, out GameObject stateObj))
        {
            if (newState == currentState)
                return;
            
            if (prevObj != null)
                prevObj.SetActive(false);

            stateObj.SetActive(true);
            prevObj = stateObj;
            currentState = newState;

            AnnounceMainMenuState?.Invoke(currentState);
            
        //    Debug.Log(newState);
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

    public void TestEndScreen()
    {
        ChangeState(MainMenuStates.EndGame);
    }

    void OnDisable()
    {
    }
}