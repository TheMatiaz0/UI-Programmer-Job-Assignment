using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Flags]
public enum PopupContentType
{
    None = 0,
    Controls = 1 << 0,
    Graphics = 1 << 1,
    Audio = 1 << 2,
    Language = 1 << 3,
    Gameplay = 1 << 4,
    Accessibility = 1 << 5,
    Credits = 1 << 6,
}

public class DynamicPopup : Popup
{
    [Header("Dynamic Popup")]
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private List<PopupContent> allContents;

    private PopupContent selectedContent;

    public void SetupContent(PopupContentType contentType)
    {
        if (!allContents.Exists(x => (x.ContentType & contentType) != 0))
        {
            contentType = PopupContentType.None;
        }
        DisplayContent(contentType);
    }

    private void DisplayContent(PopupContentType selected)
    {
        foreach (var content in allContents)
        {
            content.gameObject.SetActive((content.ContentType & selected) != 0);
            if ((content.ContentType & selected) != 0)
            {
                title.SetText(content.Title);
                selectedContent = content;
                content.OnCancelled += OnCancelled;
            }
            else
            {
                content.OnCancelled -= OnCancelled;
            }
        }
    }

    public override void CloseItself()
    {
        CloseItself(selectedContent.Navigator);
    }

    private void OnCancelled(PopupContent content)
    {
        CloseItself();
    }
}
