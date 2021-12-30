using System;
using System.Collections;
using System.Collections.Generic;
using Script.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoSingleton<LoadSceneManager>
{
    private static int stageCount = 0;
    private const string LOAD_SCENE_NAME = "Loading Scene";

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadScene(string nextScene)
    {
        SceneManager.LoadScene(LOAD_SCENE_NAME);
    }
}
