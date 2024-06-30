using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewspaperDataHolder : MonoBehaviour
{
    public NewspaperData NewspaperData;

    private Image _image;

    public int Cost;

    private void Start()
    {
        _image = GetComponent<Image>();

        _image.sprite = NewspaperData.Sprite;
    }
}
