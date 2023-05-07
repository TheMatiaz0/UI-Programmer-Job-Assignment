using UnityEngine;
using UnityEngine.UI;

public class MenuButtonsController : MonoBehaviour
{
    [SerializeField]
    private Button settings, quit;
    [SerializeField]
    private CanvasGroup mainView;

    private void Awake()
    {
        settings.onClick.AddListener(OpenSettings);
        quit.onClick.AddListener(Application.Quit);
    }

    public void OpenSettings() 
    {
        mainView.blocksRaycasts = false;
        PopupManager.Instance.OpenPopup(PopupType.Settings, OnPopupOpened);
    }

    private void OnPopupOpened(Popup popup)
    {
        popup.OnClose += OnPopupClosed;
    }

    private void OnPopupClosed(Popup popup)
    {
        popup.OnClose -= OnPopupClosed;
        mainView.blocksRaycasts = true;
    }
}
