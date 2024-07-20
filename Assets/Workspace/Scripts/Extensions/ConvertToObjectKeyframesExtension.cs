using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ConvertToObjectKeyframesExtension
{
    public static ObjectReferenceKeyframe[] ToObjectKeyframes<T>(this T[] objects, float frameRate) where T : Object
    {
        var objectKeyframes = new ObjectReferenceKeyframe[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            objectKeyframes[i] = new ObjectReferenceKeyframe
            {
                time = i / frameRate,
                value = objects[i]
            };
        }

        return objectKeyframes;
    }
}
