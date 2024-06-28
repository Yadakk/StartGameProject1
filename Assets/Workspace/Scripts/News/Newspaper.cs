using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Newspaper : MonoBehaviour, IDropHandler
{
    public readonly List<NewsDrag> AttachedNews = new();

    public void OnDrop(PointerEventData eventData)
    {
        var newsDrag = eventData.pointerDrag.GetComponent<NewsDrag>();
        newsDrag.OnStartDrag.AddListener(RemoveNews);

        eventData.pointerDrag.transform.SetParent(transform);
        AttachedNews.Add(newsDrag);
        newsDrag.Attach();
    }

    private void RemoveNews(NewsDrag newsDrag)
    {
        AttachedNews.Remove(newsDrag);
        newsDrag.OnStartDrag.RemoveListener(RemoveNews);
        newsDrag.Detach();
    }
}
