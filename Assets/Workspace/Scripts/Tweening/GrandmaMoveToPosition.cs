using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class GrandmaMoveToPosition : MonoBehaviour
{
    public GrandmaDataHolder Holder;
    [System.NonSerialized] public GrandmaGenerator Generator;
    public float AnimDuration;
    public float MessageShowTime;
    public Vector2 DefaultSize = new(100, 100);
    public Vector2 DefaultSizeGrandma = new(100, 100);

    public readonly UnityEvent OnGrandmaExit = new();

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

    public void SetSprites()
    {
        Image.sprite = Holder.GrandmaData.Sprite;
        PopupImage.sprite = Holder.GrandmaData.TextboxSprite;
        PopupRect.sizeDelta = DefaultSize * PopupImage.sprite.bounds.extents;
        Rect.sizeDelta = DefaultSizeGrandma * Image.sprite.bounds.extents / Image.sprite.bounds.extents.y;
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
        transform.DOLocalMove(destination.localPosition - new Vector3(0f, Holder.GrandmaData.DownOffset, 0f), AnimDuration).SetEase(Ease.InOutSine).OnComplete(Deconstruct);
    }

    private void Deconstruct()
    {
        UpDownTween.Stop();
        Destroy(gameObject);
        OnGrandmaExit.Invoke();
    }

    private void Stop()
    {
        UpDownTween.Stop();
        PopupTween.Appear();

        gameObject.GetComponentInParent<IsGrandmaWaiting>().IsWaiting = true;
    }

    public void GetPaper(bool isAngry)
    {
        if (isAngry)
        {
            PopupTween.Disappear(EndMessage);

            gameObject.GetComponentInParent<IsGrandmaWaiting>().IsWaiting = false;
        }
        else
        {
            PopupTween.Disappear(GoToExit);
            gameObject.GetComponentInParent<IsGrandmaWaiting>().IsWaiting = false;
        }
    }

    private void EndMessage()
    {
        Image.sprite = Holder.GrandmaData.AngrySprite;
        PopupImage.sprite = Holder.GrandmaData.AngryTextboxSprite;

        PopupRect.sizeDelta = DefaultSize * PopupImage.sprite.bounds.extents;
        Rect.sizeDelta = DefaultSizeGrandma * Image.sprite.bounds.extents / Image.sprite.bounds.extents.y;

        PopupTween.Appear(() => StartCoroutine(EndMessageDisappear()));
    }

    private IEnumerator EndMessageDisappear()
    {
        yield return new WaitForSeconds(MessageShowTime);
        PopupTween.Disappear(GoToExit);
    }

    private void GoToExit()
    {
        MoveToEnd(Generator.Exit);
        UpDownTween.Play();
    }
}
