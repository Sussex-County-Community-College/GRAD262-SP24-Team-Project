using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : Singleton<MySceneManager>
{
    private void Start()
    {
        DockingAssist dockingAssist = FindAnyObjectByType<DockingAssist>();

        if (dockingAssist)
        {
            dockingAssist.onDockingStateChange.AddListener(OnDockingStateChange);
        }
    }

    private void OnDockingStateChange(DockingAssist.DockingState dockingState)
    {
        if (dockingState == DockingAssist.DockingState.docked || dockingState == DockingAssist.DockingState.undocking)
        {
            ChangeScene();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        int nextScene = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextScene);
    }
}