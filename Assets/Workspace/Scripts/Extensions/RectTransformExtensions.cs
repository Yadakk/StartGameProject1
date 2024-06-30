using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public static class RectTransformExtensions
{
    /// <summary>
    /// Set pivot without changing the position of the element
    /// </summary>
    public static void SetPivot(this RectTransform rectTransform, Vector2 pivot)
    {
        if (rectTransform == null) return;

        Vector2 size = rectTransform.rect.size;
        Vector2 deltaPivot = rectTransform.pivot - pivot;
        Vector3 deltaPosition = rectTransform.rotation * new Vector3((deltaPivot.x * size.x) * rectTransform.localScale.x, (deltaPivot.y * size.y) * rectTransform.localScale.y);
        rectTransform.pivot = pivot;
        rectTransform.localPosition -= deltaPosition;
    }

    public static Rect RectTransformToScreenSpace(this RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }

    public static void KeepFullyInRect(this RectTransform rect, RectTransform parentRect)
    {
        Vector2 pos = rect.localPosition;

        Vector2 minPosition = parentRect.rect.min - rect.rect.min;
        Vector2 maxPosition = parentRect.rect.max - rect.rect.max;

        pos.x = Mathf.Clamp(rect.localPosition.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(rect.localPosition.y, minPosition.y, maxPosition.y);

        rect.localPosition = pos;
    }

    public static void KeepFullyInRect(this RectTransform rect, RectTransform parentRect, float animDuration)
    {
        Vector2 pos = rect.localPosition;

        Vector2 minPosition = parentRect.rect.min - rect.rect.min * rect.lossyScale;
        Vector2 maxPosition = parentRect.rect.max - rect.rect.max * rect.lossyScale;

        pos.x = Mathf.Clamp(rect.localPosition.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(rect.localPosition.y, minPosition.y, maxPosition.y);

        if (rect.localPosition == (Vector3)pos) return;
        rect.DOLocalMove(pos, animDuration).SetEase(Ease.OutExpo);
    }

    public static void ResizeFullyInRect(this RectTransform rect, RectTransform parentRect)
    {
        Vector2 originalPivot = rect.pivot;
        rect.SetPivot(new(0f, 0f));

        Vector2 maxSize = parentRect.rect.max - rect.anchoredPosition;
        Vector2 newSize = rect.sizeDelta;

        if (rect.sizeDelta.x > maxSize.x) newSize.x = maxSize.x;
        if (rect.sizeDelta.y > maxSize.y) newSize.y = maxSize.y;

        rect.SetPivot(new(1f, 1f));
        maxSize = rect.anchoredPosition - parentRect.rect.min;

        if (rect.sizeDelta.x > maxSize.x) newSize.x = maxSize.x;
        if (rect.sizeDelta.y > maxSize.y) newSize.y = maxSize.y;

        rect.SetPivot(originalPivot);
        rect.sizeDelta = newSize;
    }

    public static void WrapInRect(this RectTransform rect, RectTransform parentRect)
    {
        Vector2 pos = rect.localPosition;

        Vector2 minPosition = parentRect.rect.min - rect.rect.min;
        Vector2 maxPosition = parentRect.rect.max - rect.rect.max;

        if (pos.x > maxPosition.x) pos.x = minPosition.x;
        if (pos.x < minPosition.x) pos.x = maxPosition.x;
        if (pos.y > maxPosition.y) pos.y = minPosition.y;
        if (pos.y < minPosition.y) pos.y = maxPosition.y;

        rect.localPosition = pos;
    }
}