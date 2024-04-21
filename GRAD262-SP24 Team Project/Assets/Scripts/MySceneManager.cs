using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : Singleton<MySceneManager>
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            int nextScene = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(nextScene);
        }
    }
}