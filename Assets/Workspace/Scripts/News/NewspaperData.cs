using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NewspaperData", menuName = "ScriptableObjects/NewspaperData", order = 2)]
public class NewspaperData : ScriptableObject
{
    public Sprite Sprite;
    public Theme Theme;
}