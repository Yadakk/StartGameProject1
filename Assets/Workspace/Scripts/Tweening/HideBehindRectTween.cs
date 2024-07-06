using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HideBehindRectTween : MonoBehaviour
{
    public bool StartHidden = true;
    public float AnimSeconds = 1f;
    public Ease AnimEaseShow;
    public Ease AnimEaseHide;

    private Vector2 _initialPosition;

    private RectTransform _rectTransform;
    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
        set => _rectTransform = value;
    }

    public void Hide()
    {
        RectTransform.DOAnchorPosY(-RectTransform.rect.yMin, AnimSeconds).SetEase(AnimEaseHide);
    }

    public void Hide(TweenCallback tweenCallback)
    {
        RectTransform.DOAnchorPosY(-RectTransform.rect.yMin, AnimSeconds).SetEase(AnimEaseHide).OnComplete(tweenCallback);
    }

    public void Show()
    {
        RectTransform.DOAnchorPosY(_initialPosition.y, AnimSeconds).SetEase(AnimEaseShow);
    }

    public void Show(TweenCallback tweenCallback)
    {
        RectTransform.DOAnchorPosY(_initialPosition.y, AnimSeconds).SetEase(AnimEaseShow).OnComplete(tweenCallback);
    }

    private void Awake()
    {
        _initialPosition = RectTransform.anchoredPosition;
    }

    private void Start()
    {
        if (StartHidden) RectTransform.anchoredPosition = new(0f, -RectTransform.rect.yMin);
    }
}