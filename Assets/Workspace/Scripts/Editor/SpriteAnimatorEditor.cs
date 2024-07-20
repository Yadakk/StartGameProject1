using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class SpriteAnimatorEditor
{
    #region AssignToImage
    [MenuItem("Assets/Animate sprites from folder/To Image", true)]
    private static bool AnimateSpritesFromFolderToImageValidation()
    {
        return Selection.activeObject is AnimationClip;
    }

    [MenuItem("Assets/Animate sprites from folder/To Image")]
    private static void AnimateSpritesFromFolderToImage()
    {
        FillAnimationClipWithSpritesFromFolder(new EditorCurveBinding().BindToImageSprite());
    }
    #endregion

    #region AssignToSpriteRenderer
    [MenuItem("Assets/Animate sprites from folder/To SpriteRenderer", true)]
    private static bool AnimateSpritesFromFolderToSpriteRendererValidation()
    {
        return Selection.activeObject is AnimationClip;
    }

    [MenuItem("Assets/Animate sprites from folder/To SpriteRenderer")]
    private static void AnimateSpritesFromFolderToSpriteRenderer()
    {
        FillAnimationClipWithSpritesFromFolder(new EditorCurveBinding().BindToSpriteRendererSprite());
    }
    #endregion

    #region CalculationMethods
    private static void FillAnimationClipWithSpritesFromFolder(EditorCurveBinding curveBinding)
    {
        GetAnimationClip(out var selectedAnimationClip, out var selectedAnimationClipPathRelative);

        string spriteFolderPathAbsolute = GetSpriteFolderPath(selectedAnimationClipPathRelative);
        if (string.IsNullOrEmpty(spriteFolderPathAbsolute)) return;

        Sprite[] sprites = GetSpritesFromFolder(spriteFolderPathAbsolute);

        selectedAnimationClip.frameRate = sprites.Length;
        BindSpritesToAnimationClip(selectedAnimationClip, sprites, curveBinding);

        SaveAndRefreshDatabase();
    }

    private static void BindSpritesToAnimationClip(AnimationClip selectedAnimationClip, Sprite[] sprites, EditorCurveBinding curveBinding)
    {
        ObjectReferenceKeyframe[] spriteKeyFrames = sprites.ToObjectKeyframes(sprites.Length);
        AnimationUtility.SetObjectReferenceCurve(selectedAnimationClip, curveBinding, spriteKeyFrames);
    }

    private static void SaveAndRefreshDatabase()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static Sprite[] GetSpritesFromFolder(string spriteFolderPathAbsolute)
    {
        string spriteFolderPathRelative = spriteFolderPathAbsolute.AbsoluteToRelativePath();
        Sprite[] sprites = spriteFolderPathRelative.GetAssetsFromFolder<Sprite>().ToArray();
        return sprites;
    }

    private static string GetSpriteFolderPath(string selectedAnimationClipPathRelative)
    {
        string defaultFolderPanelPath = selectedAnimationClipPathRelative.RelativeToAbsolutePath();
        string spriteFolderPathAbsolute = EditorUtility.OpenFolderPanel("Select image folder", defaultFolderPanelPath, "");
        return spriteFolderPathAbsolute;
    }

    private static void GetAnimationClip(out AnimationClip selectedAnimationClip, out string selectedAnimationClipPathRelative)
    {
        selectedAnimationClip = Selection.activeObject as AnimationClip;
        selectedAnimationClipPathRelative = AssetDatabase.GetAssetPath(selectedAnimationClip);
    }
    #endregion
}