using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AppearingButton : MonoBehaviour
{
    public float AnimDuration = 1f;

    private Image _image;
    public Image Image { get => _image ? _image : _image = GetComponent<Image>(); set => _image = value; }

    private void Start()
    {
        Image = GetComponent<Image>();
    }

    public void Appear()
    {
        gameObject.SetActive(true);
        Image.DOColor(Color.white, AnimDuration);
    }
}
