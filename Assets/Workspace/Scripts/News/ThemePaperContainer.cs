using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class ThemePaperContainer : MonoBehaviour
{
    public RectTransform Bounds;
    public Theme Theme;

    private GameObject _newspaper;
    public GameObject Newspaper
    {
        get => _newspaper;
        set
        {
            _newspaper = value;
            var drag = _newspaper.GetComponent<NewspaperDrag>();
            var canvasGroup = _newspaper.GetComponent<CanvasGroup>();

            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            drag.GetComponent<NewspaperDrag>().DragEnabled = true;

            drag.OnDeconstruct.AddListener(OnPaperDeconstruct);
            drag.Bounds = Bounds;
        }
    }

    private void OnPaperDeconstruct(NewspaperDrag newspaper)
    {
        var newpaper = Instantiate(newspaper.gameObject, transform.position, transform.rotation, transform);
        newpaper.transform.SetParent(transform.parent, true);

        Newspaper = newpaper;
    }
}