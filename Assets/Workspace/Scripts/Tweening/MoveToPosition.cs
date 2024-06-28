using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveToPosition : MonoBehaviour
{
    public float AnimDuration;

    private UpDownTween _upDownTween;
    public UpDownTween UpDownTween
    {
        get
        {
            if (_upDownTween == null) _upDownTween = GetComponent<UpDownTween>();
            return _upDownTween;
        }
        set => _upDownTween = value;
    }

    private PopupTween _popupTween;
    public PopupTween PopupTween
    {
        get
        {
            if (_popupTween == null) _popupTween = GetComponentInChildren<PopupTween>();
            return _popupTween;
        }
        set => _popupTween = value;
    }

    public void Move(Transform destination)
    {
        transform.SetParent(destination, true);
        transform.DOLocalMove(destination.localPosition, AnimDuration).SetEase(Ease.InOutSine).OnComplete(Stop);
    }

    private void Stop()
    {
        UpDownTween.Stop();
        PopupTween.Appear();

        gameObject.GetComponentInParent<IsGrandmaWaiting>().IsWaiting = true;
    }
}
