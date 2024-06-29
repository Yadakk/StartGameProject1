using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    public PlayerValues PlayerValues;

    private TextMeshProUGUI _tmpu;

    void Start()
    {
        _tmpu = GetComponent<TextMeshProUGUI>();
        PlayerValues.Values.OnMoneyChanged.AddListener(UpdateCounter);

        UpdateCounter(PlayerValues.Values.Money);
    }

    public void UpdateCounter(int amount)
    {
        _tmpu.text = amount.ToString();
    }
}