using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteAssetNames
{
    private static readonly string[] _assetRichTexts = new string[]
    {
        @"<sprite=""MoneyIcon"" name=""MoneyIcon"">",
    };

    public enum AssetName
    {
        MoneyIcon,
    }

    public static string GetRichText(AssetName assetName) => _assetRichTexts[(int)assetName];
}