using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    public static UnityEvent OnSceneChangeStarted = new();

    public static void LoadScene(string sceneName)
    {
        if (DOTween.TotalActiveTweens() > 0)
        {
            var pausedTweens = DOTween.PausedTweens();
            var playingTweens = DOTween.PlayingTweens();

            KillTweens(pausedTweens);
            KillTweens(playingTweens);
        }

        OnSceneChangeStarted.Invoke();
        SceneManager.LoadScene(sceneName);
    }

    private static void KillTweens(List<Tween> tweens)
    {
        if (tweens == null) return;
        for (int i = 0; i < tweens.Count; i++)
            tweens[i]?.Kill();
    }

    public static void LoadScene(string sceneName, GameObject loadingScreen)
    {
        loadingScreen.SetActive(true);
        LoadScene(sceneName);
    }
}
