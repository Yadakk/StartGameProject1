using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ThrowToPeople : MonoBehaviour, IDropHandler
{
    public PlayerValues PlayerValues;
    public Transform ThrowDestination;
    public ExpenditureCounter VipExpenditure;
    public ExpenditureCounter TotalExpenditure;
    public RectTransform ThrowTo;
    public NewspaperGenerator NewspaperSource;
    public AudioSource AudioSource;
    public PopupTutorial PopupTutorialOld;
    public PopupTutorial PopupTutorialOld2;
    public PopupTutorial PopupTutorialOld3;
    public PopupTutorial PopupTutorial;
    public PopupTutorial LowQualityPopupTutorial;
    public int LowQualityCost = 10;
    public int MidQualityCost = 15;
    public int HighQualityCost = 20;
    public int VipCost = 3;
    public float AnimDuration = 1f;

    private ThemePaperContainer[] _themePaperContainers;

    private void Start()
    {
        _themePaperContainers = ThrowTo.GetComponentsInChildren<ThemePaperContainer>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!eventData.pointerDrag.TryGetComponent<NewspaperDataHolder>(out var holder)) return;

        PopupTutorialOld.Disappear();
        PopupTutorialOld2.Disappear();
        PopupTutorialOld3.Disappear();
        PopupTutorial.Appear();

        ThemePaperContainer selectedContainer = null;
        AudioSource.PlayOneShot(AudioSource.clip);

        for (int i = 0; i < _themePaperContainers.Length; i++)
        {
            if (_themePaperContainers[i].Theme != holder.NewspaperData.Theme) continue;
            selectedContainer = _themePaperContainers[i];
            break;
        }

        var attachedFragments = holder.GetComponentsInChildren<FragmentDataHolder>();
        bool allThemesCorrect = true;
        bool hasVipNews = false;

        foreach (var fragment in attachedFragments)
        {
            fragment.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        if (attachedFragments.Length < 3) allThemesCorrect = false;
        else
            foreach (var fragment in attachedFragments)
            {
                if (fragment.FragmentData.Theme != holder.NewspaperData.Theme) { allThemesCorrect = false; break; }
                if (fragment.FragmentData.IsVip)
                {
                    hasVipNews = true;
                    PlayerValues.Values.Money -= VipCost;
                    VipExpenditure.Expenditure += VipCost;
                    TotalExpenditure.Expenditure -= VipCost;
                }
            }

        if (!allThemesCorrect) { holder.Cost = LowQualityCost; LowQualityPopupTutorial.Appear(); }
        else if (!hasVipNews) holder.Cost = MidQualityCost;
        else holder.Cost = HighQualityCost;

        if (PlayerValues.Values.Money < 0) return;

        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = false;
        eventData.pointerDrag.GetComponent<CanvasGroup>().alpha = 1f;
        eventData.pointerDrag.GetComponent<NewspaperDrag>().DragEnabled = false;
        var gameObject = eventData.pointerDrag;
        NewspaperSource.Generate();
        eventData.pointerDrag.transform.SetAsLastSibling();
        eventData.pointerDrag.transform.DOLocalMoveX(ThrowDestination.localPosition.x, AnimDuration).SetEase(Ease.InExpo).OnComplete(() => AddPaperToShelves(gameObject, selectedContainer));
    }

    private void AddPaperToShelves(GameObject gameObject, ThemePaperContainer selectedContainer)
    {
        var newpaper = Instantiate(gameObject);
        newpaper.transform.SetParent(selectedContainer.transform);
        newpaper.transform.position = selectedContainer.transform.position;
        newpaper.transform.SetParent(selectedContainer.Bounds);

        selectedContainer.Newspaper = newpaper;
    }
}
