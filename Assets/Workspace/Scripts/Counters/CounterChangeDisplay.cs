using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CounterChangeDisplay : MonoBehaviour
{
    public MoneyCounter MoneyCounter;

    private TextMeshProUGUI _tmpu;

    private void Start()
    {
        _tmpu = GetComponent<TextMeshProUGUI>();
    }
}
