using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GrandmaData", menuName = "ScriptableObjects/GrandmaData", order = 3)]
public class GrandmaData : ScriptableObject
{
    public Sprite Sprite;
    public Theme Theme;
}