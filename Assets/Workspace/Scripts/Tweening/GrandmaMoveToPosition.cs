using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class GrandmaMoveToPosition : MonoBehaviour
{
    [System.NonSerialized] public GrandmaGenerator Generator;
    public float AnimDuration;
    public float MessageShowTime;

    public readonly UnityEvent OnGrandmaExit = new();

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

    public void MoveToEnd(Transform destination)
    {
        transform.SetParent(destination, true);
        transform.DOLocalMove(destination.localPosition, AnimDuration).SetEase(Ease.InOutSine).OnComplete(Deconstruct);
    }

    private void Deconstruct()
    {
        UpDownTween.Stop();
        Destroy(gameObject);
        OnGrandmaExit.Invoke();
    }

    private void Stop()
    {
        UpDownTween.Stop();
        PopupTween.Appear();

        gameObject.GetComponentInParent<IsGrandmaWaiting>().IsWaiting = true;
    }

    public void GetPaper()
    {
        PopupTween.Disappear(EndMessage);

        gameObject.GetComponentInParent<IsGrandmaWaiting>().IsWaiting = false;
    }

    private void EndMessage()
    {
        PopupTween.Appear(() => StartCoroutine(EndMessageDisappear()));
    }

    private IEnumerator EndMessageDisappear()
    {
        yield return new WaitForSeconds(MessageShowTime);
        PopupTween.Disappear(GoToExit);
    }

    private void GoToExit()
    {
        MoveToEnd(Generator.Exit);
        UpDownTween.Play();
    }
}
