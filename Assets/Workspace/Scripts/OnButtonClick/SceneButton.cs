using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : OnButtonClick
{
    public string SceneName;
    public GameObject LoadingScreen;

    void Start()
    {
        AddListener(LoadScene);
    }

    public void LoadScene()
    {
        if (LoadingScreen != null) LoadingScreen.SetActive(true);
        SceneManager.LoadScene(SceneName);
    }
}
