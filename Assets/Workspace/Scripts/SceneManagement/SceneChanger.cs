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
            var tweens = DOTween.PausedTweens();
            if (tweens == null) tweens = DOTween.PlayingTweens();
            else tweens.AddRange(DOTween.PlayingTweens());
            for (int i = 0; i < tweens.Count; i++)
                tweens[i]?.Kill();
        }

        OnSceneChangeStarted.Invoke();
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadScene(string sceneName, GameObject loadingScreen)
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
}
