using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameFlower : MonoBehaviour
{
    public PlayerValues PlayerValues;
    public GrandmaGenerator Generator;
    public OpenCloseWindow ResultsWindow;
    public GameObject ThemePaperContainerParent;
    public ExpenditureCounter PrinterExpenditure;
    public ExpenditureCounter VipExpenditure;
    public ExpenditureCounter IncomeExpenditure;
    public ExpenditureCounter TotalExpenditure;
    public GameObject LoadingScreen;
    public int PrinterCost = 35;

    public readonly UnityEvent OnNewDay = new();

    public int GrandmasPerDay = 6;
    public int MaxDays = 3;
    public string WinSceneName;

    public int CurrentDay { get; private set; }

    private int _grandmasToGo;

    ThemePaperContainer[] _themePaperContainers;

    private void Start()
    {
        _themePaperContainers = ThemePaperContainerParent.GetComponentsInChildren<ThemePaperContainer>();

        ResultsWindow.CloseAction = StartDay;
        StartDay();
    }

    private void StartDay()
    {
        PrinterExpenditure.Expenditure = 0;
        VipExpenditure.Expenditure = 0;
        IncomeExpenditure.Expenditure = 0;
        TotalExpenditure.Expenditure = 0;

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
        if (CurrentDay >= MaxDays) { SceneChanger.LoadScene(WinSceneName, LoadingScreen); return; }

        PlayerValues.Values.Money -= PrinterCost;
        PrinterExpenditure.Expenditure += PrinterCost;
        TotalExpenditure.Expenditure -= PrinterCost;

        if (PlayerValues.Values.Money < 0) return;

        ResultsWindow.Open();

        foreach (var item in _themePaperContainers)
        {
            if (item.Newspaper != null) Destroy(item.Newspaper);
        }
    }
}