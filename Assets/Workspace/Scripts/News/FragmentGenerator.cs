using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class FragmentGenerator : MonoBehaviour
{
    [System.NonSerialized] public NewspaperDataHolder CurrentPaper;
    public Transform KeepZone;
    public GameObject Prefab;
    public List<FragmentData> FragmentDatas;

    public List<FragmentData> VipFragmentDatas { get => FragmentDatas.Where(data => data.Theme == CurrentPaper.NewspaperData.Theme && data.IsVip).ToList(); }
    public List<FragmentData> AcceptableFragmentDatas { get => FragmentDatas.Where(data => data.Theme == CurrentPaper.NewspaperData.Theme && !data.IsVip).ToList(); }
    public List<FragmentData> FloodFragmentDatas { get => FragmentDatas.Where(data => data.Theme == Theme.Flood && !data.IsVip).ToList(); }

    public int GenerateAcceptable = 3;
    public int GenerateUnacceptable = 3;

    private RectTransform _rectTransform;
    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null) RectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
        set => _rectTransform = value;
    }

    public void Generate(FragmentData fragmentData)
    {
        GameObject news = Instantiate(Prefab, transform);
        var newsRect = news.GetComponent<RectTransform>();
        newsRect.sizeDelta *= fragmentData.Sprite.bounds.extents;
        newsRect.anchoredPosition = new(Random.Range(RectTransform.rect.min.x + newsRect.rect.max.x, RectTransform.rect.max.x - newsRect.rect.max.x),
                                        Random.Range(RectTransform.rect.min.y + newsRect.rect.max.y, RectTransform.rect.max.y - newsRect.rect.max.y));

        news.transform.SetParent(KeepZone, true);
        var holder = news.GetComponent<FragmentDataHolder>();
        holder.FragmentData = fragmentData;
    }

    public void GenerateMany()
    {
        List<FragmentData> generatedDatas = new();

        for (int i = 0; i < GenerateAcceptable; i++)
        {
            var acceptable = AcceptableFragmentDatas.Where(item => !generatedDatas.Contains(item)).ToList();
            var data = acceptable[Random.Range(0, acceptable.Count)];
            Generate(data);
            generatedDatas.Add(data);
        }

        for (int i = 0; i < GenerateUnacceptable; i++)
        {
            var acceptable = FloodFragmentDatas.Where(item => !generatedDatas.Contains(item)).ToList();
            var data = acceptable[Random.Range(0, acceptable.Count)];
            Generate(data);
            generatedDatas.Add(data);
        }
    }

    public FragmentData GenerateVip()
    {
        var data = VipFragmentDatas[Random.Range(0, VipFragmentDatas.Count)];
        Generate(data);
        return data;
    }
}
