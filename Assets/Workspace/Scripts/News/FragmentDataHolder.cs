using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FragmentDataHolder : MonoBehaviour
{
    public FragmentData FragmentData;

    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = FragmentData.Sprite;
    }
}
