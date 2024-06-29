using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameFlower : MonoBehaviour
{
    public GrandmaGenerator Generator;
    public OpenCloseWindow ResultsWindow;

    public readonly UnityEvent OnNewDay = new();

    public int GrandmasPerDay = 6;
    public int MaxDays = 3;
    public string WinSceneName;

    public int CurrentDay { get; private set; }

    private int _grandmasToGo;

    private void Start()
    {
        ResultsWindow.CloseAction = StartDay;
        StartDay();
    }

    private void StartDay()
    {
        if (CurrentDay >= MaxDays) { SceneChanger.LoadScene(WinSceneName); return; }
        _grandmasToGo = GrandmasPerDay;
        GenerateGrandma();
        CurrentDay++;
        OnNewDay.Invoke();
    }

    private void GenerateGrandma()
    {
        if (_grandmasToGo == 0) { EndDay(); return; }
        var grandma = Generator.Generate();
        _grandmasToGo--;
        grandma.GetComponent<GrandmaMoveToPosition>().OnGrandmaExit.AddListener(GenerateGrandma);
    }

    private void EndDay()
    {
        ResultsWindow.Open();
    }
}