using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    public PlayerValues PlayerValues;

    public readonly UnityEvent<int> OnValueChanged = new();
    public readonly UnityEvent<int> OnValueChangedDifference = new();

    private TextMeshProUGUI _tmpu;
    public TextMeshProUGUI Tmpu { get => _tmpu; set => _tmpu = value; }

    private int _prevAmount;

    void Start()
    {
        Tmpu = GetComponent<TextMeshProUGUI>();
        PlayerValues.Values.OnMoneyChanged.AddListener(val => UpdateCounter(val, true, false));
        _prevAmount = PlayerValues.Values.Money;

        UpdateCounter(PlayerValues.Values.Money, false, true);
    }

    public void UpdateCounter(int amount, bool invokeEvents, bool updateText)
    {
        if (updateText) UpdateText();
        if (!invokeEvents || amount == _prevAmount) return;

        OnValueChanged.Invoke(amount);
        OnValueChangedDifference.Invoke(amount - _prevAmount);

        _prevAmount = amount;
    }

    public void UpdateText()
    {
        Tmpu.text = PlayerValues.Values.Money.ToString();
    }
}