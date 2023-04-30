using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class TweenData
{
    [SerializeField]
    private Ease ease;
    [SerializeField]
    private float duration;

    public Ease Ease => ease;
    public float Duration => duration;
}
