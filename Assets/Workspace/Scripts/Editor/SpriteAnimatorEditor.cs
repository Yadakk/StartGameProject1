using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class SpriteAnimatorEditor
{
    [MenuItem("Assets/Assign sprites from folder", true)]
    private static bool AssignSpritesFromFolderValidation()
    {
        return Selection.activeObject is AnimationClip;
    }

    [MenuItem("Assets/Assign sprites from folder")]
    private static void AssignSpritesFromFolder()
    {
        AnimationClip clip = Selection.activeObject as AnimationClip;
        var clipPath = AssetDatabase.GetAssetPath(clip);
        var defaultPath = Path.GetDirectoryName(Path.GetFullPath(Path.Combine(Application.dataPath, @"../")) + clipPath);
        string texturesPath = EditorUtility.OpenFolderPanel("Select image folder", defaultPath, "");
        if (string.IsNullOrEmpty(texturesPath)) return;

        texturesPath = AbsoluteToRelative(texturesPath);

        var sprites = GetAssetsFromFolder<Sprite>(texturesPath).ToArray();
        clip.frameRate = sprites.Length;

        EditorCurveBinding spriteBinding = CreateSpriteBinding();

        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[sprites.Length];
        spriteKeyFrames = SetSpriteKeyframes(sprites, spriteKeyFrames, clip.frameRate);
        AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, spriteKeyFrames);

        SaveAssets();
    }

    private static string AbsoluteToRelative(string path)
    {
        if (path.StartsWith(Application.dataPath))
            path = "Assets" + path[Application.dataPath.Length..];

        return path;
    }

    private static void SaveAssets()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static ObjectReferenceKeyframe[] 
    SetSpriteKeyframes(Sprite[] sprites, ObjectReferenceKeyframe[] spriteKeyFrames, float frameRate)
    {
        for (int i = 0; i < spriteKeyFrames.Length; i++)
        {
            spriteKeyFrames[i] = new ObjectReferenceKeyframe
            {
                time = i / frameRate,
                value = sprites[i]
            };
        }

        return spriteKeyFrames;
    }

    private static EditorCurveBinding CreateSpriteBinding()
    {
        return new()
        {
            type = typeof(Image),
            path = "",
            propertyName = "m_Sprite"
        };
    }

    private static T[] GetAssetsFromFolder<T>(string path) where T : Object
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