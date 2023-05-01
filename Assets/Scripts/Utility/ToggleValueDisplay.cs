using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleValueDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private Toggle toggle;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(UpdateText);
    }

    private void UpdateText(bool value)
    {
        text.SetText(value ? "ON" : "OFF");
    }
}
