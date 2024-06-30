using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class NewspaperDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float DragAlpha = 0.4f;
    [System.NonSerialized] public RectTransform Bounds;
    public float BoundsAnimDuration = 1f;
    public bool DragEnabled = true;

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

    public readonly UnityEvent<NewspaperDrag> OnStartDrag = new();
    public readonly UnityEvent<NewspaperDrag> OnDeconstruct = new();

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Deconstruct()
    {
        OnDeconstruct.Invoke(this);
        Destroy(gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.DOKill();
        transform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = DragAlpha;
        OnStartDrag.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!DragEnabled) return;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;

        if (_rectTransform == null) return;
        if (Bounds == null) return;
        _rectTransform.KeepFullyInRect(Bounds, BoundsAnimDuration);
    }
}