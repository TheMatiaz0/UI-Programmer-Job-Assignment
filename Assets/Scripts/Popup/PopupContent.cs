using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupContent : MonoBehaviour, ICancelHandler
{
    public event Action<PopupContent> OnCancelled = delegate { };

    [SerializeField]
    private PopupContentType contentType;
    [SerializeField]
    private string title;
    [SerializeField]
    private UINavigationManager navigator;

    public PopupContentType ContentType => contentType;
    public string Title => title;
    public UINavigationManager Navigator => navigator;

    public void OnCancel(BaseEventData eventData)
    {
        OnCancelled(this);
    }
}
