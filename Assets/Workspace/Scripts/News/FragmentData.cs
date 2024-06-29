using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FragmentData", menuName = "ScriptableObjects/FragmentData", order = 1)]
public class FragmentData : ScriptableObject
{
    public Sprite Sprite;
    public Theme Theme;
    public bool IsVip;
}