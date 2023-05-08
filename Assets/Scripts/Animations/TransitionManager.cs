using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        SetVisibility(true);
    }

    public void ChangeSceneToMenu()
    {
        SetVisibility(false, () => SceneManager.LoadScene("MainMenu"));
    }

    private void SetVisibility(bool isVisible, Action callback = null)
    {
        var seq = DOTween.Sequence();
        seq.Append(canvasGroup.DOFade(isVisible ? 1 : 0, exitTweenData.Duration).SetEase(exitTweenData.Ease));
        seq.Append(audioSource.DOFade(isVisible ? cachedAudioVolume : 0, exitTweenData.Duration).SetEase(exitTweenData.Ease))
            .OnComplete(() => callback?.Invoke());
    }
}
