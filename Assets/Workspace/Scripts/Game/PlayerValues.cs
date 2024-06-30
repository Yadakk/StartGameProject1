using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerValues : MonoBehaviour
{
    public ValuesToSave Values;
    public string GameOverSceneName;
    public GameObject LoadingScreen;
    public GameSaver Saver;

    private void Start()
    {
        Values.OnMoneyLessThanZero.AddListener(OnLose);
    }

    private void OnLose()
    {
        Saver.Delete();
        SceneChanger.LoadScene(GameOverSceneName, LoadingScreen);
    }
}
