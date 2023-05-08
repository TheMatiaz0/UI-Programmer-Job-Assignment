using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialAnimation : MonoBehaviour
{
    [SerializeField]
    private RectTransform radialRt;
    [SerializeField]
    private RectTransform line;
    [SerializeField]
    private float radialTime;
    [SerializeField]
    private float lineTime;

    private void Update()
    {
        radialRt.transform.rotation = Quaternion.Euler(0, Time.time * radialTime * 360, 0);
        line.transform.localRotation = Quaternion.Euler(0, 0, Time.time * lineTime * 360);
    }
}
