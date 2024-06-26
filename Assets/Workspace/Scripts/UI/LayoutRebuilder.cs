using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class LayoutRebuilder : MonoBehaviour
{
    [FormerlySerializedAs("_refreshOnStart")]
    [SerializeField] private bool _rebuildOnStart;
    RectTransform _rect;

    void Start()
    {
        GetRect();
        if (_rebuildOnStart) Rebuild();
    }

    public void Rebuild()
    {
        GetRect();
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);
    }

    private void GetRect()
    {
        if (_rect == null) _rect = GetComponent<RectTransform>();
    }
}
