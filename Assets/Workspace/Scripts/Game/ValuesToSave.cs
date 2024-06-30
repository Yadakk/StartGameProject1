using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ValuesToSave
{
    public readonly UnityEvent<int> OnMoneyChanged = new();
    public readonly UnityEvent OnMoneyLessThanZero = new();
    [SerializeField] private int _money;
    public int Money
    {
        get => _money;
        set
        {
            if (value < 0) OnMoneyLessThanZero.Invoke();
            OnMoneyChanged.Invoke(value);
            _money = value;
        }
    }

    public int MoneyForSaving;
}
