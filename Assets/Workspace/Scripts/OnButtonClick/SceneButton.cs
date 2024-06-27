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
        AddListener(() => SceneChanger.LoadScene(SceneName, LoadingScreen));
    }
}