using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonsController : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;

    private void Awake()
    {
        foreach (var button in buttons) 
        {
            button.onClick.AddListener(OpenDetails);
        }
    }

    private void OpenDetails()
    {
        PopupManager.Instance.OpenPopup(PopupType.DetailedSettings);
    }
}
