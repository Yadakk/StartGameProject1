using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTo : MonoBehaviour
{
    public Vector3 StartPos;
    public Vector3 EndPos;
    public float AnimSpeed = 1f;

    public void Move()
    {
        transform.DOLocalMove(EndPos, AnimSpeed);
    }

    public void ResetPos()
    {
        transform.localPosition = StartPos;
    }
}
