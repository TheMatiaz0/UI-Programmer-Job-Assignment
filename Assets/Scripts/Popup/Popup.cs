using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Popup : MonoBehaviour, ICancelHandler
{
    public event Action<Popup> OnClose = delegate { };

    [SerializeField]
    private PopupType type;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Button closeButton;
    [Header("Can be left null if dynamic content")]
    [SerializeField]
    private UINavigationManager navigator;

    public PopupType Type => type;
    public CanvasGroup CanvasGroup => canvasGroup;

    private void Awake()
    {
        closeButton?.onClick.AddListener(CloseItself);
    }

    public void CloseItself(UINavigationManager navigator)
    {
        OnClose(this);
        navigator.GoBack();
    }

    public virtual void CloseItself()
    {
        CloseItself(navigator);
    }

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
