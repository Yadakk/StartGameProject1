using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadScene(string sceneName, GameObject loadingScreen)
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
}
