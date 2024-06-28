using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpDownTween : MonoBehaviour
{
    public float Offset = -10f;
    public float MoveSeconds = 1f;

    private Sequence _mySequence;

    private void Start()
    {
        _mySequence = DOTween.Sequence().SetLoops(-1, LoopType.Yoyo);
        _mySequence.Append(transform.DOLocalMoveY(Offset, MoveSeconds).SetEase(Ease.InOutSine));
        _mySequence.Play();
    }

    public void Stop()
    {
        _mySequence.OnStepComplete(() => _mySequence.Kill());
    }
}
