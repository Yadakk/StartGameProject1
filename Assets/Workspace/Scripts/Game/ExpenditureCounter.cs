using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpenditureCounter : MonoBehaviour
{
    private int _expenditure;
    public int Expenditure
    {
        get => _expenditure;
        set
        {
            _expenditure = value;
            UpdateCounter(_expenditure);
        }
    }

    private TextMeshProUGUI _tmpu;
    public TextMeshProUGUI Tmpu
    {
        get
        {
            if (_tmpu == null) _tmpu = GetComponent<TextMeshProUGUI>();
            return _tmpu;
        }
        set => _tmpu = value;
    }

    void Start()
    {
        UpdateCounter(Expenditure);
    }

    public void UpdateCounter(int amount)
    {
        Tmpu.text = amount.ToString();
    }
}
