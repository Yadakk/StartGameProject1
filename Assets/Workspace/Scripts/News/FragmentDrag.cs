using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class FragmentDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float DragAlpha = 0.4f;
    public float BoundsAnimDuration = 1f;

    private Canvas _canvas;
    public Canvas Canvas 
    {
        get
        {
            if (_canvas == null) _canvas = GetComponentInParent<Canvas>();
            return _canvas;
        }
        private set => _canvas = value; 
    }

    private RectTransform _canvasRect;
    public RectTransform CanvasRect
    {
        get
        {
            if (_canvasRect == null) _canvasRect = Canvas.GetComponent<RectTransform>();
            return _canvasRect;
        }
        private set => _canvasRect = value;
    }

    private Image _image;
    public Image Image
    {
        get
        {
            if (_image == null) _image = GetComponent<Image>();
            return _image;
        }
        private set => _image = value;
    }

    public readonly UnityEvent<FragmentDrag> OnStartDrag = new();

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private bool _isAttached;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Attach()
    {
        Image.color = Color.green;
        _isAttached = true;
    }

    public void Detach()
    {
        Image.color = Color.white;
        transform.SetParent(transform.parent.parent);
        _isAttached = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!TransitionRelatively.CanTrigger || Time.timeScale == 0) return;
        _rectTransform.DOKill();
        transform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = DragAlpha;
        OnStartDrag.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!TransitionRelatively.CanTrigger || Time.timeScale == 0) return;
        _rectTransform.anchoredPosition += eventData.delta / Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!TransitionRelatively.CanTrigger || Time.timeScale == 0) return;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;

        if (_isAttached) return;

        _rectTransform.KeepFullyInRect(CanvasRect, BoundsAnimDuration);
    }
}
