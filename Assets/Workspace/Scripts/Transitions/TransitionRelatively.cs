using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransitionRelatively : MonoBehaviour
{
    public RectTransform RectTransform;
    public Vector2 MoveMultiplier;
    public float AnimDuration;

    private static bool _canTrigger = true;

    private void MoveUiElement(Vector2 pos)
    {
        RectTransform.DOAnchorPos(pos, AnimDuration).SetEase(Ease.OutCubic);
    }

    public void Move()
    {
        if (!_canTrigger) return;
        StartCoroutine(Cooldown());
        MoveUiElement(RectTransform.anchoredPosition + RectTransform.rect.max * MoveMultiplier * 2);
    }

    private IEnumerator Cooldown()
    {
        _canTrigger = false;
        yield return new WaitForSeconds(AnimDuration);
        _canTrigger = true;
    }
}
