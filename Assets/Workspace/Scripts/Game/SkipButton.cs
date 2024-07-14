using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkipButton : MonoBehaviour
{
    public AppearingButton AppearingButton;
    public PopupTutorial Tutorial;
    public float AnimDuration = 1f;

    private Image _image;
    public Image Image { get => _image ? _image : _image = GetComponent<Image>(); set => _image = value; }

    private Button _button;
    public Button Button { get => _button ? _button : _button = GetComponent<Button>(); set => _button = value; }

    private void Start()
    {
        Button.onClick.AddListener(ActivateControls);
        Button.onClick.AddListener(Disappear);
        Image.color = Color.clear;
    }

    public void ActivateControls()
    {
        AppearingButton.Appear();
        Disappear();
        Tutorial.Appear();
    }

    public void Appear()
    {
        DOTween.Kill(transform);
        gameObject.SetActive(true);
        Image.DOColor(Color.white, AnimDuration);
    }

    public void Disappear()
    {
        DOTween.Kill(transform);
        Button.interactable = false;
        Image.DOColor(Color.clear, AnimDuration);
    }
}
