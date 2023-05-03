using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Popup : MonoBehaviour, ICancelHandler
{
    public event Action<CanvasGroup> OnClose = delegate { };

    [SerializeField]
    private PopupType type;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Button closeButton;

    public PopupType Type => type;
    public CanvasGroup CanvasGroup => canvasGroup;

    private void Awake()
    {
        closeButton?.onClick.AddListener(CloseItself);
    }

    public void CloseItself() => OnClose(canvasGroup);

    public void OnCancel(BaseEventData eventData)
    {
        CloseItself();
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (canvasGroup == null)
        {
            canvasGroup = this.GetComponent<CanvasGroup>();
        }
    }

#endif
}
