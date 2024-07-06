using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static SpriteAssetNames;

[RequireComponent(typeof(TextMeshProUGUI))]
public class VipCostCounter : MonoBehaviour
{
    public ThrowToPeople ThrowToPeople;

    private TextMeshProUGUI _tmpu;

    private void Start()
    {
        _tmpu = GetComponent<TextMeshProUGUI>();
        _tmpu.text = ThrowToPeople.VipCost + GetRichText(AssetName.MoneyIcon);
    }
}
