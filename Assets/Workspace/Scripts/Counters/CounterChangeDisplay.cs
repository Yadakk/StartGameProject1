using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CounterChangeDisplay : MonoBehaviour
{
    public MoneyCounter MoneyCounter;
    public Color NegativeColor;
    public Color PositiveColor;
    public float FadeSpeedStart = 0.2f;
    public float DisplayTime = 3f;

    private RectTransform _rectTransform;
    public RectTransform RectTransform
    {
        get => _rectTransform == null ? _rectTransform = GetComponent<RectTransform>() : _rectTransform;
        private set => _rectTransform = value;
    }

    private TextMeshProUGUI _tmpu;
    public TextMeshProUGUI Tmpu
    {
        get => _tmpu == null ? _tmpu = GetComponent<TextMeshProUGUI>() : _tmpu;
        private set => _tmpu = value;
    }

    private TweenToAnchored _tweenToAnchored;
    public TweenToAnchored TweenToAnchored
    {
        get => _tweenToAnchored == null ? _tweenToAnchored = GetComponent<TweenToAnchored>() : _tweenToAnchored;
        private set => _tweenToAnchored = value;
    }

    private RectTransform _counterRect;
    public RectTransform CounterRect
    {
        get => _counterRect == null ? _counterRect = MoneyCounter.GetComponent<RectTransform>() : _counterRect;
        private set => _counterRect = value;
    }

    private bool _isShown;
    private bool _isDisappearing;
    private float _displayTimeRemaining;
    private int _displayedValue;
    private bool _timerActive;

    private void Start()
    {
        _displayTimeRemaining = DisplayTime;
        TweenToAnchored.SetStartPosToCurrent();
        MoneyCounter.OnValueChangedDifference.AddListener(Appear);
    }

    private void Update()
    {
        if (_timerActive) _displayTimeRemaining -= Time.deltaTime;
        if (_displayTimeRemaining < 0) Disappear();
    }

    public void Appear(int difference)
    {
        _displayedValue += difference;
        Tmpu.color = _displayedValue > 0 ? PositiveColor : NegativeColor;
        Tmpu.text = _displayedValue > 0 ? "+" + _displayedValue.ToString() : _displayedValue.ToString();

        _timerActive = true;

        if (_isShown) return;
        _isShown = true;

        Tmpu.alpha = 0f;
        Tmpu.DOFade(1f, FadeSpeedStart).SetEase(Ease.OutExpo);
    }

    public void Disappear()
    {
        _displayTimeRemaining = DisplayTime;
        _timerActive = false;

        if (_isDisappearing) return;
        _isDisappearing = true;

        TweenToAnchored.Move(new(RectTransform.anchoredPosition.x, CounterRect.anchoredPosition.y), OnDisappear, Ease.InExpo);
        Tmpu.DOFade(0f, TweenToAnchored.AnimSpeed).SetEase(Ease.InCirc); 
    }

    private void OnDisappear()
    {
        _displayedValue = 0;
        TweenToAnchored.ResetPos();
        MoneyCounter.UpdateText();
        MoneyCounter.transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutSine).OnComplete(() => MoneyCounter.transform.DOScale(1f, 0.2f).SetEase(Ease.InSine));

        _isShown = false;
        _isDisappearing = false;
    }
}
