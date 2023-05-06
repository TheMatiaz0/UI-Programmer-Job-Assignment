using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum PopupType
{
    Settings,
    DetailedSettings,
    MainMenu
}

public class PopupManager : MonoBehaviour
{
    [SerializeField]
    private PopupType defaultPopupToOpen;
    [SerializeField]
    private List<Popup> allPopups;
    [SerializeField]
    private TweenData openAnimation;
    [SerializeField]
    private TweenData closeAnimation;

    private List<CanvasGroup> openedPopups = new();
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
            if (popup.Type != defaultPopupToOpen)
            {
                ClosePopup(popup.CanvasGroup);
            }
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
        if (openedPopups.Contains(popupToOpen.CanvasGroup))
        {
            return;
        }
        OpenPopup(popupToOpen.CanvasGroup);
        callback?.Invoke(popupToOpen);
    }

    public void OpenPopup(CanvasGroup canvasGroup)
    {
        closeTween?.Kill();
        if (openedPopups.Count >= 1)
        {
            var latestPopup = openedPopups[^1];
            latestPopup.blocksRaycasts = false;
        }
        canvasGroup.gameObject.SetActive(true);
        openTween?.Kill(true);
        openedPopups.Add(canvasGroup);
        openTween = canvasGroup.transform.DOScale(Vector3.one, openAnimation.Duration)
            .SetEase(openAnimation.Ease).SetTarget(this);
    }

    public void ClosePopup(PopupType type)
    {
        var popupToClose = allPopups.Find(x => x.Type == type);
        ClosePopup(popupToClose.CanvasGroup);
    }

    public void ClosePopup(CanvasGroup canvasGroup)
    {
        openTween?.Kill();
        int previousIndex = openedPopups.FindIndex(x => x == canvasGroup) - 1;
        if (previousIndex > -1 && openedPopups.Count > previousIndex)
        {
            openedPopups[previousIndex].blocksRaycasts = true;
        }
        closeTween?.Kill(true);
        openedPopups.Remove(canvasGroup);
        closeTween = canvasGroup.transform.DOScale(Vector3.zero, closeAnimation.Duration)
            .SetEase(closeAnimation.Ease)
            .OnKill(() => FinalizePopup(canvasGroup))
            .OnComplete(() => FinalizePopup(canvasGroup)).SetTarget(this);
    }

    private void FinalizePopup(CanvasGroup canvasGroup)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.gameObject.SetActive(false);
    }

    public void CloseAllPopups()
    {
        openedPopups = new();
        foreach (var popup in allPopups)
        {
            ClosePopup(popup.CanvasGroup);
        }
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
