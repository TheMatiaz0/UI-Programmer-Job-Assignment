using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonsController : MonoBehaviour
{
    [SerializeField]
    private Button startGame, settings, quit;

    private void Awake()
    {
        startGame.onClick.AddListener(StartGame);
        settings.onClick.AddListener(OpenSettings);
        quit.onClick.AddListener(Application.Quit);
    }

    public void StartGame()
    {
    }

    public void OpenSettings() 
    {
        PopupManager.Instance.OpenPopup(PopupType.Settings);
    }
}
