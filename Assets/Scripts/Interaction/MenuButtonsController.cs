using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonsController : MonoBehaviour
{
    [SerializeField]
    private Button startGame, settings, quit;
    [SerializeField]
    private GameObject settingsWindow;
    [SerializeField]
    private GameObject saveSlot;

    private void Awake()
    {
        startGame.onClick.AddListener(StartGame);
        settings.onClick.AddListener(OpenSettings);
        quit.onClick.AddListener(Application.Quit);
    }

    public void StartGame()
    {
        EventSystem.current.SetSelectedGameObject(saveSlot);
    }

    public void OpenSettings() 
    {
        PopupManager.Instance.OpenPopup(PopupType.Settings);
    }
}
