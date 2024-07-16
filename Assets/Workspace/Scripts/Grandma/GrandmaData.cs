using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GrandmaData", menuName = "ScriptableObjects/GrandmaData", order = 3)]
public class GrandmaData : ScriptableObject
{
    public RuntimeAnimatorController AnimatorController;
    public Sprite TextboxSprite;
    public Sprite AngryTextboxSprite;
    public Theme Theme;
    public float DownOffset = 60f;
}