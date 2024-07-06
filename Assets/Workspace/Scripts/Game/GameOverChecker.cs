using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    public PlayerValues PlayerValues;
    public string GameOverSceneName;
    public GameObject LoadingScreen;
    public GameSaver Saver;

    public void Check()
    {
        if (PlayerValues.Values.Money >= 0) return;
        Saver.Delete();
        SceneChanger.LoadScene(GameOverSceneName, LoadingScreen);
    }
}
