using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private TweenData enterTweenData;
    [SerializeField]
    private TweenData exitTweenData;

    public static TransitionManager Instance { get; private set; }

    private float cachedAudioVolume;

    private void Awake()
    {
        Instance = this;
        cachedAudioVolume = audioSource.volume;
        canvasGroup.alpha = 0;
        audioSource.volume = 0;
        SetVisibility(true, true);
    }

    public void ChangeSceneToMenu()
    {
        SetVisibility(false, callback: () => SceneManager.LoadScene("MainMenu"));
    }

    private void SetVisibility(bool shouldBeVisible, bool isAlwaysInteractive = false, Action callback = null)
    {
        if (!isAlwaysInteractive)
        {
            SetInteractive(shouldBeVisible);
        }
        var seq = DOTween.Sequence();
        seq.Append(canvasGroup.DOFade(shouldBeVisible ? 1 : 0, exitTweenData.Duration)
            .SetEase(exitTweenData.Ease)
            .OnComplete(() => 
            { 
                if (!isAlwaysInteractive)
                {
                    SetInteractive(!shouldBeVisible);
                }
            }));
        seq.Append(audioSource.DOFade(shouldBeVisible ? cachedAudioVolume : 0, exitTweenData.Duration)
            .SetEase(exitTweenData.Ease))
            .OnComplete(() => callback?.Invoke()).SetUpdate(true);
    }

    private void SetInteractive(bool shouldBeInteractive)
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.enabled = shouldBeInteractive;
        }
    }
}
