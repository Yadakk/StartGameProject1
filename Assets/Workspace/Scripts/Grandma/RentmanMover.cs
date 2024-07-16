using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RentmanMover : MonoBehaviour
{
    public GrandmaDataHolder Holder;
    public SkipButton SkipButton;
    [System.NonSerialized] public GrandmaGenerator Generator;
    public float AnimDuration;
    public float DisappearAnimDuration;
    public float MessageShowTime;
    public Vector2 DefaultSize = new(100, 100);

    private UpDownTween _upDownTween;
    public UpDownTween UpDownTween
    {
        get
        {
            if (_upDownTween == null) _upDownTween = GetComponent<UpDownTween>();
            return _upDownTween;
        }
        set => _upDownTween = value;
    }

    private PopupTween _popupTween;
    public PopupTween PopupTween
    {
        get
        {
            if (_popupTween == null) _popupTween = GetComponentInChildren<PopupTween>();
            return _popupTween;
        }
        set => _popupTween = value;
    }

    private Image _popupImage;
    public Image PopupImage
    {
        get
        {
            if (_popupImage == null) _popupImage = PopupTween.GetComponent<Image>();
            return _popupImage;
        }
        set => _popupImage = value;
    }

    private RectTransform _popupRect;
    public RectTransform PopupRect
    {
        get
        {
            if (_popupRect == null) _popupRect = PopupTween.GetComponent<RectTransform>();
            return _popupRect;
        }
        set => _popupRect = value;
    }

    private RectTransform _rect;
    public RectTransform Rect
    {
        get
        {
            if (_rect == null) _rect = GetComponent<RectTransform>();
            return _rect;
        }
        set => _rect = value;
    }

    private Image _image;
    public Image Image
    {
        get
        {
            if (_image == null) _image = GetComponent<Image>();
            return _image;
        }
        set => _image = value;
    }

    private void Start()
    {
        SkipButton.Button.onClick.AddListener(Disappear);
    }

    public void SetSprites()
    {
        PopupImage.sprite = Holder.GrandmaData.TextboxSprite;
        PopupRect.sizeDelta = DefaultSize * PopupImage.sprite.bounds.extents;
    }

    public void Move(Transform destination)
    {
        transform.SetParent(destination, true);
        transform.DOLocalMove(destination.localPosition - new Vector3(0f, Holder.GrandmaData.DownOffset, 0f), AnimDuration).SetEase(Ease.InOutSine).OnComplete(Stop);
        UpDownTween.Play();
    }

    public void MoveToEnd(Transform destination)
    {
        transform.SetParent(destination, true);
        transform.DOLocalMove(destination.localPosition - new Vector3(0f, Holder.GrandmaData.DownOffset, 0f), AnimDuration).SetEase(Ease.InOutSine).OnComplete(() => Deconstruct(true));
    }

    private void Deconstruct(bool activateControls)
    {
        UpDownTween.Stop();
        transform.DOKill();
        Destroy(gameObject);
        if (activateControls) SkipButton.ActivateControls();
    }

    private void Stop()
    {
        UpDownTween.Stop();
        PopupTween.Appear();
        StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine()
    {
        yield return new WaitForSeconds(MessageShowTime);
        HideMessage();
    }

    public void HideMessage()
    {
        PopupTween.Disappear(GoToExit);
    }

    private void GoToExit()
    {
        MoveToEnd(Generator.Exit);
        UpDownTween.Play();
    }

    private void Disappear()
    {
        Image.DOColor(Color.clear, DisappearAnimDuration).OnComplete(() => Deconstruct(false));
        PopupTween.GetComponent<Image>().DOColor(Color.clear, DisappearAnimDuration);
    }
}
