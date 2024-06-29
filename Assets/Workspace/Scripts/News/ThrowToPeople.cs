using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowToPeople : MonoBehaviour, IDropHandler
{
    public GameObject MiniPaper;
    public RectTransform ThrowTo;
    public NewspaperSource NewspaperSource;

    public void OnDrop(PointerEventData eventData)
    {
        if (!eventData.pointerDrag.TryGetComponent<Newspaper>(out _)) return;
        var minipaper = Instantiate(MiniPaper, ThrowTo);
        eventData.pointerDrag.transform.SetParent(minipaper.transform);
        eventData.pointerDrag.SetActive(false);
        NewspaperSource.Generate();
    }
}
