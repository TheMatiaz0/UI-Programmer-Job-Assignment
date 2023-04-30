using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PopupContentType
{
    None = 0,
    Controls = 1,
    Graphics = 2,
    Audio = 3,
    Language = 4,
    Gameplay = 5,
    Accessibility = 6,
    Credits = 7,
}

[Serializable]
public class PopupContent
{
    [SerializeField]
    private PopupContentType contentType;
    [SerializeField] 
    private string title;
    [SerializeField]
    private GameObject gameObject;

    public PopupContentType ContentType => contentType;
    public string Title => title;
    public GameObject GameObject => gameObject;
}

public class DynamicPopup : Popup
{
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private List<PopupContent> allContents;

    public void Setup(PopupContentType contentType)
    {
        if (!allContents.Exists(x => x.ContentType == contentType))
        {
            contentType = PopupContentType.None;
        }
        DisplayContent(contentType);
    }

    private void DisplayContent(PopupContentType selected)
    {
        foreach (var content in allContents)
        {
            content.GameObject.SetActive(content.ContentType == selected);
            if (content.ContentType == selected)
            {
                title.SetText(content.Title);
            }
        }
    }
}
