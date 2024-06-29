using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Minipaper : MonoBehaviour
{
    private RectTransform _rectTransform;
    private RectTransform _parentRectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _parentRectTransform = transform.parent.GetComponent<RectTransform>();

        _rectTransform.anchoredPosition = new(Random.Range(_parentRectTransform.rect.min.x + _rectTransform.rect.max.x, _parentRectTransform.rect.max.x - _rectTransform.rect.max.x),
                                              Random.Range(_parentRectTransform.rect.min.y + _rectTransform.rect.max.y, _parentRectTransform.rect.max.y - _rectTransform.rect.max.y));
        transform.SetParent(transform.parent.parent, true);
    }
}
