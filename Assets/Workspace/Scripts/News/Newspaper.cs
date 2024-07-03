using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Newspaper : MonoBehaviour, IDropHandler
{
    public readonly List<FragmentDrag> AttachedNews = new();
    [System.NonSerialized] public PopupTutorial TutorialLinkOld;
    [System.NonSerialized] public PopupTutorial TutorialLink;
    [System.NonSerialized] public PopupTutorial TutorialLink2;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent<Newspaper>(out _)) return;
        if (!TransitionRelatively.CanTrigger || Time.timeScale == 0) return;
        if (AttachedNews.Count >= 3) return;
        var newsDrag = eventData.pointerDrag.GetComponent<FragmentDrag>();
        newsDrag.OnStartDrag.AddListener(RemoveNews);
        { TutorialLink.Appear(); TutorialLinkOld.Disappear(); }
        if (AttachedNews.Count >= 2) { TutorialLink2.Appear(); TutorialLink.Disappear(); }

        eventData.pointerDrag.transform.SetParent(transform);
        AttachedNews.Add(newsDrag);
        newsDrag.Attach();
    }

    private void RemoveNews(FragmentDrag newsDrag)
    {
        AttachedNews.Remove(newsDrag);
        newsDrag.OnStartDrag.RemoveListener(RemoveNews);
        newsDrag.Detach();
    }
}
