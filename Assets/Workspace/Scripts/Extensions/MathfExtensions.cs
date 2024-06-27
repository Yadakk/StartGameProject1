using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public static class MathfExtensions
{
    public static float LerpAudio(float t)
    {
        return Mathf.Lerp(-80f, 0f, Mathf.Pow(t, 0.25f));
    }
}