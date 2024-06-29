using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class OpenCloseWindow : MonoBehaviour
{
    public float AnimSeconds = 1f;
    public UnityAction CloseAction;

    public void Open()
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, AnimSeconds).SetEase(Ease.OutExpo);
    }

    public void Close()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(Vector3.zero, AnimSeconds).SetEase(Ease.InExpo).OnComplete(OnClosed);
    }

    private void OnClosed()
    {
        gameObject.SetActive(false);
        CloseAction?.Invoke();
    }
}
