using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemePaperContainer : MonoBehaviour
{
    public Theme Theme;

    private GameObject _newspaper;
    public GameObject Newspaper
    {
        get => _newspaper;
        set
        {
            _newspaper = value;
            _newspaper.GetComponent<NewspaperDrag>().OnDeconstruct.AddListener(OnPaperDeconstruct);
        }
    }

    private void OnPaperDeconstruct(NewspaperDrag newspaper)
    {
        var newpaper = Instantiate(newspaper.gameObject, transform.position, transform.rotation, transform);
        newpaper.transform.SetParent(transform.parent, true);

        var canvasGroup = newpaper.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Newspaper = newpaper;
    }
}