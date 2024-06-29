using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThrowToPeople : MonoBehaviour, IDropHandler
{
    public RectTransform ThrowTo;
    public NewspaperGenerator NewspaperSource;
    public float SizeMultiplier = 0.3f;
    public int LowQualityCost = 10;
    public int MidQualityCost = 15;
    public int HighQualityCost = 20;

    private ThemePaperContainer[] _themePaperContainers;

    private void Start()
    {
        _themePaperContainers = ThrowTo.GetComponentsInChildren<ThemePaperContainer>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!eventData.pointerDrag.TryGetComponent<NewspaperDataHolder>(out var holder)) return;

        ThemePaperContainer selectedContainer = null;

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
            fragment.GetComponent<Image>().raycastTarget = false;
            if (fragment.FragmentData.Theme != holder.NewspaperData.Theme) { allThemesCorrect = false; break; }
            if (fragment.FragmentData.IsVip) hasVipNews = true;
        }

        if (!allThemesCorrect) holder.Cost = LowQualityCost;
        else if (!hasVipNews) holder.Cost = MidQualityCost;
        else holder.Cost = HighQualityCost;

        eventData.pointerDrag.transform.SetParent(selectedContainer.transform);
        eventData.pointerDrag.transform.position = selectedContainer.transform.position;
        eventData.pointerDrag.transform.localScale *= SizeMultiplier;
        eventData.pointerDrag.transform.SetParent(selectedContainer.transform.parent);

        selectedContainer.Newspaper = eventData.pointerDrag;

        NewspaperSource.Generate();
    }
}
