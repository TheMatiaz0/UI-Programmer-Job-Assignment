using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ButtonContentType
{
    [SerializeField]
    private PopupContentType contentType;
    [SerializeField]
    private Button button;

    public PopupContentType ContentType => contentType;
    public Button Button => button;
}

public class SettingsButtonsController : MonoBehaviour
{
    [SerializeField]
    private ButtonContentType[] buttons;

    private void Awake()
    {
        foreach (var button in buttons) 
        {
            button.Button.onClick.AddListener(() => OpenDetails(button.ContentType));
        }
    }

    private void OpenDetails(PopupContentType contentType)
    {
        PopupManager.Instance.OpenPopup(PopupType.DetailedSettings, (popup) => (popup as DynamicPopup).SetupContent(contentType));
    }
}
