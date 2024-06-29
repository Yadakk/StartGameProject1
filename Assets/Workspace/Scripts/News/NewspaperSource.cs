using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewspaperSource : MonoBehaviour
{
    public GameObject Prefab;
    public Transform Destination;
    public float AnimSeconds;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        var paper = Instantiate(Prefab, transform.position, transform.rotation, transform);
        paper.transform.SetParent(transform.parent, true);
        paper.transform.DOLocalMove(Destination.localPosition, AnimSeconds).SetEase(Ease.OutCirc);
    }    
}
