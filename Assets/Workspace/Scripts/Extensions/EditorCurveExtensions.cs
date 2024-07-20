using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public static class EditorCurveExtensions
{
    public static EditorCurveBinding BindToImageSprite(this EditorCurveBinding editorCurveBinding)
    {
        editorCurveBinding.type = typeof(Image);
        editorCurveBinding.path = "";
        editorCurveBinding.propertyName = "m_Sprite";

        return editorCurveBinding;
    }

    public static EditorCurveBinding BindToSpriteRendererSprite(this EditorCurveBinding editorCurveBinding)
    {
        editorCurveBinding.type = typeof(SpriteRenderer);
        editorCurveBinding.path = "";
        editorCurveBinding.propertyName = "m_Sprite";

        return editorCurveBinding;
    }
}
