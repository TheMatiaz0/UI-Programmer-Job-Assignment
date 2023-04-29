using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public enum PopupType
{
    Settings,
}

public class PopupManager : MonoBehaviour
{
    [SerializeField]
    private List<Popup> allPopups;
    [SerializeField]
    private TweenData openAnimation;
    [SerializeField]
    private TweenData closeAnimation;

    private Tween currentTween;

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

    public void OpenPopup(PopupType type)
    {
        var popupToOpen = allPopups.Find(x => x.Type == type);
        OpenPopup(popupToOpen.CanvasGroup);
    }

    public void OpenPopup(CanvasGroup canvasGroup)
    {
        canvasGroup.gameObject.SetActive(true);
        currentTween.Kill();
        currentTween = canvasGroup.transform.DOScale(Vector3.one, openAnimation.Duration).SetEase(openAnimation.Ease);
    }

    public void ClosePopup(PopupType type)
    {
        var popupToClose = allPopups.Find(x => x.Type == type);
        ClosePopup(popupToClose.CanvasGroup);
    }

    public void ClosePopup(CanvasGroup canvasGroup)
    {
        currentTween.Kill();
        currentTween = canvasGroup.transform.DOScale(Vector3.zero, closeAnimation.Duration)
            .SetEase(closeAnimation.Ease)
            .OnComplete(() => canvasGroup.gameObject.SetActive(false));
    }

    public void CloseAllPopups()
    {
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
