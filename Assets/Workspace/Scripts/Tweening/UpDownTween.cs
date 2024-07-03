using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class UpDownTween : MonoBehaviour
{
    public float Offset = -10f;
    public float MoveSeconds = 1f;

    private Sequence _mySequence;

    public void Stop()
    {
        _mySequence.OnStepComplete(() => _mySequence.Pause());
    }

    public void Play()
    {
        _mySequence = DOTween.Sequence().SetLoops(-1, LoopType.Yoyo);
        _mySequence.Append(transform.DOLocalMoveY(transform.localPosition.y + Offset, MoveSeconds).SetEase(Ease.InOutSine));
        _mySequence.Play();
    }
}
