using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static SortedSet<string> instantiated = new SortedSet<string>();
    public List<int> disableOnScene;

    void Awake()
    {
        if (instantiated.Contains(name))
        {
            Destroy(gameObject);
        }
        else
        {
            instantiated.Add(name);
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += OnSceneChanged;
        }
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        if (disableOnScene.Contains(next.buildIndex))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
