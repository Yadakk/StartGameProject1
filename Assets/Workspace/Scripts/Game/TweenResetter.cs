using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenResetter : MonoBehaviour
{
    void Start()
    {
        if (DOTween.TotalActiveTweens() > 0)
        {
            var tweens = DOTween.PausedTweens();
            if (tweens == null) tweens = DOTween.PlayingTweens();
            else tweens.AddRange(DOTween.PlayingTweens());
            if (tweens == null) return;
            for (int i = 0; i < tweens.Count; i++)
                tweens[i]?.Kill();
        }
    }
}
