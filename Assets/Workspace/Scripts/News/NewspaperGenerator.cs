using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewspaperGenerator : MonoBehaviour
{
    public GameObject Prefab;
    public Transform Destination;
    public float AnimSeconds;
    public List<NewspaperData> NewspaperDatas;
    public int NewspapersPerDay = 3;
    [System.NonSerialized] public int CurrentPaperIndex;
    public int VipNewsMin = 3;
    public int VipNewsMax = 4;
    public FragmentGenerator FragmentGenerator;
    public GameFlower GameFlower;

    private int[] _vipsDistribution;

    private void Start()
    {
        //OnNewDay();
        GameFlower.OnNewDay.AddListener(OnNewDay);
    }

    private void OnNewDay()
    {
        _vipsDistribution = new int[Random.Range(VipNewsMin, VipNewsMax + 1)];

        for (int i = 0; i < _vipsDistribution.Length; i++)
        {
            _vipsDistribution[i] = Random.Range(0, NewspapersPerDay);
        }

        CurrentPaperIndex = 0;
        Generate();
    }

    public void Generate()
    {
        ClearOldFragments();

        var paper = Instantiate(Prefab, transform.position, transform.rotation, transform);
        var holder = paper.GetComponent<NewspaperDataHolder>();
        holder.NewspaperData = NewspaperDatas[Random.Range(0, NewspaperDatas.Count)];
        paper.transform.SetParent(transform.parent, true);

        FragmentGenerator.CurrentPaper = holder;
        FragmentGenerator.GenerateMany();

        for (int i = 0; i < _vipsDistribution.Length; i++)
            if (_vipsDistribution[i] == CurrentPaperIndex)
                FragmentGenerator.GenerateVip();

        paper.transform.DOLocalMove(Destination.localPosition, AnimSeconds).SetEase(Ease.OutCirc);
        CurrentPaperIndex++;
    }

    private void ClearOldFragments()
    {
        var oldFragments = transform.parent.GetComponents<FragmentDataHolder>();

        foreach (var item in oldFragments)
        {
            Destroy(item.gameObject);
        }
    }
}
