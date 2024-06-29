using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class FragmentGenerator : MonoBehaviour
{
    public NewspaperDataHolder CurrentPaper;
    public GameObject Prefab;
    public List<FragmentData> FragmentDatas;
    public List<FragmentData> VipFragmentDatas { get => FragmentDatas.Where(data => data.IsVip).ToList(); }
    public List<FragmentData> AcceptableFragmentDatas { get => FragmentDatas.Where(data => data.Theme == CurrentPaper.NewspaperData.Theme && !data.IsVip).ToList(); }
    public List<FragmentData> UnacceptableFragmentDatas { get => FragmentDatas.Where(data => data.Theme != CurrentPaper.NewspaperData.Theme && !data.IsVip).ToList(); }
    public int GenerateAcceptable = 3;
    public int GenerateUnacceptable = 3;

    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Generate(FragmentData fragmentData)
    {
        GameObject news = Instantiate(Prefab, transform);
        var newsRect = news.GetComponent<RectTransform>();
        newsRect.anchoredPosition = new(Random.Range(_rectTransform.rect.min.x + newsRect.rect.max.x, _rectTransform.rect.max.x - newsRect.rect.max.x),
                                        Random.Range(_rectTransform.rect.min.y + newsRect.rect.max.y, _rectTransform.rect.max.y - newsRect.rect.max.y));

        news.transform.SetParent(transform.parent, true);
        news.GetComponent<FragmentDataHolder>().FragmentData = fragmentData;
    }

    public void GenerateMany()
    {
        for (int i = 0; i < GenerateAcceptable; i++)
        {
            Generate(FragmentDatas[Random.Range(0, AcceptableFragmentDatas.Count)]);
        }

        for (int i = 0; i < GenerateUnacceptable; i++)
        {
            Generate(FragmentDatas[Random.Range(0, UnacceptableFragmentDatas.Count)]);
        }
    }

    public void GenerateVip()
    {
        Generate(FragmentDatas[Random.Range(0, VipFragmentDatas.Count)]);
    }
}
