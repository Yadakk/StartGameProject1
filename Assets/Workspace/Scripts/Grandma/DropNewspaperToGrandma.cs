using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropNewspaperToGrandma : MonoBehaviour, IDropHandler
{
    public IsGrandmaWaiting IsGrandmaWaiting;
    public PlayerValues PlayerValues;
    public ExpenditureCounter IncomeExpenditure;
    public ExpenditureCounter TotalExpenditure;
    public int WrongPaperCostDivision = 5;

    public void OnDrop(PointerEventData eventData)
    {
        if (!eventData.pointerDrag.TryGetComponent<NewspaperDataHolder>(out var holder)) return;
        if (!IsGrandmaWaiting.IsWaiting) return;
        var mover = IsGrandmaWaiting.GetComponentInChildren<GrandmaMoveToPosition>();
        mover.GetPaper();

        var grandmaHolder = IsGrandmaWaiting.GetComponentInChildren<GrandmaDataHolder>();
        bool reduceMoney = grandmaHolder.GrandmaData.Theme != holder.NewspaperData.Theme;
        var income = reduceMoney ? holder.Cost / WrongPaperCostDivision : holder.Cost;
        PlayerValues.Values.Money += income;
        IncomeExpenditure.Expenditure += income;
        TotalExpenditure.Expenditure += income;

        holder.GetComponent<NewspaperDrag>().Deconstruct();
    }
}