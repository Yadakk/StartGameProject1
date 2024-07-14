using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using System.Linq;

public class NewspaperGenerator : MonoBehaviour
{
    public GameObject Prefab;
    public Transform Destination;
    public float AnimSeconds;
    public List<NewspaperData> NewspaperDatas;
    public List<Theme> ThemesForDay;
    public FragmentGenerator FragmentGenerator;
    public GameFlower GameFlower;
    public RectTransform Bounds;
    public PopupTutorial TutorialLinkOld;
    public PopupTutorial TutorialLink;
    public PopupTutorial TutorialLink2;
    public ThrowToPeople ThrowToPeople;
    public PlayerValues PlayerValues;

    private List<Theme> _remainingThemes = new();

    public void StartPrinting()
    {
        _remainingThemes = ThemesForDay.ToList();
        Generate();
    }

    public void Generate()
    {
        ClearOldFragments();
        if (_remainingThemes.Count == 0) return;

        var paper = Instantiate(Prefab, transform.position, transform.rotation, transform);
        var holder = paper.GetComponent<NewspaperDataHolder>();
        var drag = paper.GetComponent<NewspaperDrag>();
        var rect = paper.GetComponent<RectTransform>();
        var newspaper = paper.GetComponent<Newspaper>();

        newspaper.ThrowToPeople = ThrowToPeople;
        newspaper.PlayerValues = PlayerValues;
        newspaper.TutorialLinkOld = TutorialLinkOld;
        newspaper.TutorialLink = TutorialLink;
        newspaper.TutorialLink2 = TutorialLink2;
        drag.Bounds = Bounds;
        var selectedTheme = _remainingThemes[Random.Range(0, _remainingThemes.Count)];
        _remainingThemes.Remove(selectedTheme);
        var themedPapers = NewspaperDatas.Where(data => data.Theme == selectedTheme).ToList();
        holder.NewspaperData = themedPapers[Random.Range(0, themedPapers.Count)];

        FragmentGenerator.CurrentPaper = holder;
        FragmentGenerator.GenerateMany();

        paper.transform.SetParent(Bounds, true);
        rect.sizeDelta *= holder.NewspaperData.Sprite.bounds.extents;

        Tweener tween = paper.transform.DOLocalMove(Destination.localPosition, AnimSeconds).SetEase(Ease.OutCirc);
    }

    private void ClearOldFragments()
    {
        for (int i = 0; i < Bounds.childCount; i++)
        {
            var child = Bounds.GetChild(i);
            if (child.TryGetComponent<FragmentDataHolder>(out _)) Destroy(child.gameObject);
        }
    }
}
