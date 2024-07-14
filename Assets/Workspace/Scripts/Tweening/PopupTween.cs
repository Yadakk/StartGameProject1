using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTween : MonoBehaviour
{
    public float Duration = 0.3f;

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

    public void Appear()
    {
        transform.DOScale(Vector3.one, Duration).SetEase(Ease.OutElastic);
    }

    public void Appear(TweenCallback action)
    {
        transform.DOScale(Vector3.one, Duration).SetEase(Ease.OutElastic).OnComplete(action);
    }

    public void Disappear()
    {
        transform.DOScale(Vector3.zero, Duration).SetEase(Ease.InOutQuint);
    }

    public void Disappear(TweenCallback action)
    {
        transform.DOScale(Vector3.zero, Duration).SetEase(Ease.InOutQuint).OnComplete(action);
    }
}