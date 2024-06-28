using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class NewsGenerator : MonoBehaviour
{
    public GameObject News;

    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        Generate(108);
    }

    public void Generate()
    {
        GameObject news = Instantiate(News, transform);
        var newsRect = news.GetComponent<RectTransform>();
        newsRect.anchoredPosition = new(Random.Range(_rectTransform.rect.min.x + newsRect.rect.max.x, _rectTransform.rect.max.x - newsRect.rect.max.x),
                                        Random.Range(_rectTransform.rect.min.y + newsRect.rect.max.y, _rectTransform.rect.max.y - newsRect.rect.max.y));

        news.transform.SetParent(transform.parent, true);
    }

    public void Generate(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Generate();
        }
    }
}
