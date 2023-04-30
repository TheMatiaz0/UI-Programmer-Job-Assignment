using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private Slider slider;

    private void Awake()
    {
        slider.onValueChanged.AddListener(UpdateText);
    }

    private void UpdateText(float value)
    {
        text.SetText($"{Mathf.RoundToInt(value * 100)}%");
    }
}
