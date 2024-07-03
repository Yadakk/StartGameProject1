using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class ThemePaperContainer : MonoBehaviour
{
    public RectTransform Bounds;
    public Theme Theme;
    public float AnimDuration = 1f;
    public float SizeMultiplier = 0.3f;

    private GameObject _newspaper;
    public GameObject Newspaper
    {
        get => _newspaper;
        set
        {
            _newspaper = value;
            var drag = _newspaper.GetComponent<NewspaperDrag>();
            var canvasGroup = _newspaper.GetComponent<CanvasGroup>();

            var oldScale = Vector3.one * SizeMultiplier;
            _newspaper.transform.localScale = Vector3.zero;
            _newspaper.transform.DOScale(oldScale, AnimDuration).OnComplete(() => EnableDrag(drag, canvasGroup));
            canvasGroup.alpha = 1f;
        }
    }

    private void EnableDrag(NewspaperDrag drag, CanvasGroup canvasGroup)
    {
        canvasGroup.blocksRaycasts = true;
        drag.DragEnabled = true;

        drag.OnDeconstruct.AddListener(OnPaperDeconstruct);
        drag.Bounds = Bounds;
    }

    private void OnPaperDeconstruct(NewspaperDrag newspaper)
    {
        var newpaper = Instantiate(newspaper.gameObject, transform.position, transform.rotation, transform);
        newpaper.transform.SetParent(Bounds, true);

        Newspaper = newpaper;
    }
}