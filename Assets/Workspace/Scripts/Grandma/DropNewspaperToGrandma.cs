using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DropNewspaperToGrandma : MonoBehaviour, IDropHandler
{
    public IsGrandmaWaiting IsGrandmaWaiting;
    public PlayerValues PlayerValues;
    public ExpenditureCounter IncomeExpenditure;
    public ExpenditureCounter TotalExpenditure;
    public int WrongPaperCostDivision = 5;
    public float AnimSeconds = 1f;
    public PopupTutorial OldPopupTutorial;
    public PopupTutorial FailPopupTutorial;

    public void OnDrop(PointerEventData eventData)
    {
        if (!eventData.pointerDrag.TryGetComponent<NewspaperDataHolder>(out var holder)) return;
        if (!IsGrandmaWaiting.IsWaiting) return;

        var canvasGroup = holder.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 1f;

        holder.GetComponent<NewspaperDrag>().DragEnabled = false;

        holder.transform.DOScale(Vector3.zero, AnimSeconds).SetEase(Ease.InExpo);
        var mover = IsGrandmaWaiting.GetComponentInChildren<GrandmaMoveToPosition>();
        var point = IsGrandmaWaiting.GetComponentInChildren<PaperAcceptPoint>();

        IsGrandmaWaiting.IsWaiting = false;

        holder.transform.DOLocalMove(point.transform.localPosition, AnimSeconds).SetEase(Ease.InExpo).OnComplete(() => GetPaper(holder, mover));
    }

    private void GetPaper(NewspaperDataHolder holder, GrandmaMoveToPosition mover)
    {
        var grandmaHolder = IsGrandmaWaiting.GetComponentInChildren<GrandmaDataHolder>();
        bool reduceMoney = grandmaHolder.GrandmaData.Theme != holder.NewspaperData.Theme;
        var income = reduceMoney ? holder.Cost / WrongPaperCostDivision : holder.Cost;

        OldPopupTutorial.Disappear();
        if (reduceMoney) FailPopupTutorial.Appear();

        PlayerValues.Values.Money += income;
        IncomeExpenditure.Expenditure += income;
        TotalExpenditure.Expenditure += income;

        mover.GetPaper(reduceMoney);

        holder.GetComponent<NewspaperDrag>().Deconstruct();
    }
}