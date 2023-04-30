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

    private List<CanvasGroup> openedPopups = new();
    private readonly Dictionary<CanvasGroup, Tween> currentTweens = new();

    public static PopupManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CloseAllPopups();
        foreach (var popup in allPopups)
        {
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
        if (openedPopups.Count >= 1)
        {
            var latestPopup = openedPopups[^1];
            latestPopup.blocksRaycasts = false;
        }
        canvasGroup.gameObject.SetActive(true);
        KillCurrentTween(canvasGroup);
        currentTweens.TryAdd(canvasGroup, canvasGroup.transform.DOScale(Vector3.one, openAnimation.Duration)
            .SetEase(openAnimation.Ease)
            .OnComplete(() => openedPopups.Add(canvasGroup)));
    }

    public void ClosePopup(PopupType type)
    {
        var popupToClose = allPopups.Find(x => x.Type == type);
        ClosePopup(popupToClose.CanvasGroup);
    }

    public void ClosePopup(CanvasGroup canvasGroup)
    {
        int previousIndex = openedPopups.FindIndex(x => x == canvasGroup) - 1;
        if (previousIndex > -1 && openedPopups.Count > previousIndex)
        {
            openedPopups[previousIndex].blocksRaycasts = true;
        }
        KillCurrentTween(canvasGroup);
        currentTweens.TryAdd(canvasGroup, canvasGroup.transform.DOScale(Vector3.zero, closeAnimation.Duration)
            .SetEase(closeAnimation.Ease)
            .OnComplete(() => FinalizePopup(canvasGroup)));
    }

    private void KillCurrentTween(CanvasGroup canvasGroup)
    {
        if (currentTweens.TryGetValue(canvasGroup, out var tween))
        {
            tween.Kill();
            currentTweens.Remove(canvasGroup);
        }
    }

    private void FinalizePopup(CanvasGroup canvasGroup)
    {
        canvasGroup.gameObject.SetActive(false);
        openedPopups.Remove(canvasGroup);
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
