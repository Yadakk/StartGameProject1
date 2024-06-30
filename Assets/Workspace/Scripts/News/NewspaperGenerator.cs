using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Linq;
using Unity.VisualScripting;

public class NewspaperGenerator : MonoBehaviour
{
    public GameObject Prefab;
    public Transform Destination;
    public float AnimSeconds;
    public List<NewspaperData> NewspaperDatas;
    public List<Theme> ThemesForDay;
    public int VipNewsMin = 1;
    public int VipNewsMax = 2;
    public FragmentGenerator FragmentGenerator;
    public GameFlower GameFlower;
    public RectTransform Bounds;

    private List<Theme> _remainingThemes = new();

    private void Start()
    {
        GameFlower.OnNewDay.AddListener(OnNewDay);
    }

    private void OnNewDay()
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

        drag.Bounds = Bounds;
        var selectedTheme = _remainingThemes[Random.Range(0, _remainingThemes.Count)];
        _remainingThemes.Remove(selectedTheme);
        var themedPapers = NewspaperDatas.Where(data => data.Theme == selectedTheme).ToList();
        holder.NewspaperData = themedPapers[Random.Range(0, themedPapers.Count)];

        paper.transform.SetParent(transform.parent, true);

        FragmentGenerator.CurrentPaper = holder;
        FragmentGenerator.GenerateMany();

        if (GameFlower.CurrentDay > 1)
            for (int i = 0; i < Random.Range(VipNewsMin, VipNewsMax + 1); i++)
            FragmentGenerator.GenerateVip();

        Tweener tween = paper.transform.DOLocalMove(Destination.localPosition, AnimSeconds).SetEase(Ease.OutCirc);
    }

    private void ClearOldFragments()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            var child = transform.parent.GetChild(i);
            if (child.TryGetComponent<FragmentDataHolder>(out _)) Destroy(child.gameObject);
        }
    }
}
