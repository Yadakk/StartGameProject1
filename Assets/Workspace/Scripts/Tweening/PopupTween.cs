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

    public void Appear()
    {
        transform.DOScale(Vector3.one, Duration).SetEase(Ease.OutElastic);
    }
}
