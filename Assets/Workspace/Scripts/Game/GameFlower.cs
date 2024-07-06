using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameFlower : MonoBehaviour
{
    public PlayerValues PlayerValues;
    public GameSaver Saver;
    public GrandmaGenerator Generator;
    public OpenCloseWindow ResultsWindow;
    public GameObject ThemePaperContainerParent;
    public ExpenditureCounter PrinterExpenditure;
    public ExpenditureCounter VipExpenditure;
    public ExpenditureCounter IncomeExpenditure;
    public ExpenditureCounter TotalExpenditure;
    public GameObject LoadingScreen;
    public NewspaperGenerator NewspaperGenerator;
    public PopupTutorial VipTutorialStart;
    public List<Sprite> DaySprites;
    public HideBehindRectTween DayPopup;
    public Image DayPopupImage;
    public int PrinterCost = 35;
    public float DayPopupShowTime = 3f;

    public readonly UnityEvent OnNewDay = new();

    public int GrandmasPerDay = 6;
    public int MaxDays = 3;
    public string WinSceneName;

    public int CurrentDay { get; set; } = 1;

    private int _grandmasToGo;

    private void Start()
    {
        ResultsWindow.CloseAction = StartDay;
    }

    public void StartDay()
    {
        PrinterExpenditure.Expenditure = 0;
        VipExpenditure.Expenditure = 0;
        IncomeExpenditure.Expenditure = 0;
        TotalExpenditure.Expenditure = 0;

        _grandmasToGo = GrandmasPerDay;
        GenerateGrandma();
        OnNewDay.Invoke();
        NewspaperGenerator.StartPrinting();

        if (CurrentDay == 2) VipTutorialStart.Appear();

        DayPopupImage.sprite = DaySprites[CurrentDay - 1];
        DayPopup.Show(() => StartCoroutine(ShowDayPopup()));
    }

    private IEnumerator ShowDayPopup()
    {
        yield return new WaitForSeconds(DayPopupShowTime);
        DayPopup.Hide();
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
        if (CurrentDay >= MaxDays) 
        { 
            Saver.Delete();
            SceneChanger.LoadScene(WinSceneName, LoadingScreen);
            return; 
        }

        PlayerValues.Values.Money -= PrinterCost;
        PrinterExpenditure.Expenditure += PrinterCost;
        TotalExpenditure.Expenditure -= PrinterCost;

        ResultsWindow.Open();

        foreach (var item in ThemePaperContainerParent.GetComponentsInChildren<Newspaper>())
        {
            Destroy(item.gameObject);
        }

        if (PlayerValues.Values.Money < 0) return;

        CurrentDay++;
        Saver.Save();
    }
}