using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class PathExtensions
{
    public static T[] GetAssetsFromFolder<T>(this string path) where T : Object
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new string[] { path });
        List<T> matchingAssets = new();

        foreach (var guid in guids)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            matchingAssets.Add(AssetDatabase.LoadAssetAtPath<T>(assetPath));
        }

        return matchingAssets.ToArray();
    }
}
