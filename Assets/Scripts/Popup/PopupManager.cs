using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum PopupType
{
    Settings,
    DetailedSettings
}

public class PopupManager : MonoBehaviour
{
    [SerializeField]
    private List<Popup> allPopups;
    [SerializeField]
    private TweenData openAnimation;
    [SerializeField]
    private TweenData closeAnimation;

    private List<Popup> openedPopups = new();
    private Tween openTween;
    private Tween closeTween;

    public static PopupManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var popup in allPopups)
        {
            ClosePopup(popup);
            popup.OnClose += ClosePopup;
        }
    }

    private void OnDestroy()
    {
        foreach (var popup in allPopups)
        {
            popup.OnClose -= ClosePopup;
        }
    }

    public void OpenPopup(PopupType type, Action<Popup> callback = null)
    {
        var popupToOpen = allPopups.Find(x => x.Type == type);
        if (openedPopups.Contains(popupToOpen))
        {
            return;
        }
        OpenPopup(popupToOpen);
        callback?.Invoke(popupToOpen);
    }

    public void OpenPopup(Popup popup)
    {
        closeTween?.Kill();
        if (openedPopups.Count >= 1)
        {
            var latestPopup = openedPopups[^1];
            latestPopup.CanvasGroup.blocksRaycasts = false;
        }
        popup.gameObject.SetActive(true);
        openTween?.Kill(true);
        openedPopups.Add(popup);
        openTween = popup.transform.DOScale(Vector2.one, openAnimation.Duration)
            .SetEase(openAnimation.Ease)
            .SetTarget(this);
    }

    public void ClosePopup(PopupType type)
    {
        var popupToClose = allPopups.Find(x => x.Type == type);
        ClosePopup(popupToClose);
    }

    public void ClosePopup(Popup popup)
    {
        openTween?.Kill();
        int previousIndex = openedPopups.FindIndex(x => x == popup) - 1;
        if (previousIndex > -1 && openedPopups.Count > previousIndex)
        {
            openedPopups[previousIndex].CanvasGroup.blocksRaycasts = true;
        }
        closeTween?.Kill(true);
        openedPopups.Remove(popup);
        closeTween = popup.transform.DOScale(Vector2.zero, closeAnimation.Duration)
            .SetEase(closeAnimation.Ease)
            .OnKill(() => FinalizePopup(popup))
            .OnComplete(() => FinalizePopup(popup))
            .SetTarget(this);
    }

    private void FinalizePopup(Popup popup)
    {
        popup.CanvasGroup.blocksRaycasts = true;
        popup.gameObject.SetActive(false);
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (allPopups == null)
        {
            allPopups = new(FindObjectsOfType<Popup>(true));
        }
    }

#endif
}
