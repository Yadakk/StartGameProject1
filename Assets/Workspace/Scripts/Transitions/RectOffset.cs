using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectOffset : MonoBehaviour
{
    public RectTransform RelativeTo;
    public Vector2 OffsetMultiplier;

    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = RelativeTo.anchoredPosition + RelativeTo.rect.max * OffsetMultiplier * 2;
    }
}