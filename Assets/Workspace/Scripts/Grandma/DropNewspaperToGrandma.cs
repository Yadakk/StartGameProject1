using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropNewspaperToGrandma : MonoBehaviour, IDropHandler
{
    public IsGrandmaWaiting IsGrandmaWaiting;

    public void OnDrop(PointerEventData eventData)
    {
        if (!eventData.pointerDrag.TryGetComponent<Minipaper>(out var minipaper)) return;
        if (!IsGrandmaWaiting.IsWaiting) return;
        var mover = IsGrandmaWaiting.GetComponentInChildren<GrandmaMoveToPosition>();
        mover.GetPaper();
        Destroy(minipaper.gameObject);
    }
}