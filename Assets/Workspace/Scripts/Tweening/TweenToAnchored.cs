using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenToAnchored : MonoBehaviour
{
    public Vector2 StartPos;
    public Vector2 EndPos;
    public float AnimSpeed = 1f;

    private RectTransform _rectTransform;
    public RectTransform RectTransform 
    {
        get => _rectTransform == null ? _rectTransform = GetComponent<RectTransform>() : _rectTransform;
        private set => _rectTransform = value;
    }

    public void SetStartPosToCurrent()
    {
        StartPos = RectTransform.anchoredPosition;
    }

    public void Move()
    {
        RectTransform.DOAnchorPos(EndPos, AnimSpeed);
    }

    public void Move(Vector2 endPos)
    {
        RectTransform.DOAnchorPos(endPos, AnimSpeed);
    }

    public void Move(Vector2 endPos, TweenCallback tweenCallback)
    {
        RectTransform.DOAnchorPos(endPos, AnimSpeed).OnComplete(tweenCallback);
    }

    public void Move(Vector2 endPos, TweenCallback tweenCallback, Ease ease)
    {
        RectTransform.DOAnchorPos(endPos, AnimSpeed).SetEase(ease).OnComplete(tweenCallback);
    }

    public void ResetPos()
    {
        RectTransform.anchoredPosition = StartPos;
    }
}