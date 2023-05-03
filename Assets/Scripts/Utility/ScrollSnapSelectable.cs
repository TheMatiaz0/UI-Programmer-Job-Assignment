using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollSnapSelectable : MonoBehaviour, ISelectHandler
{
    public event Action<ScrollSnapSelectable> OnSelected = delegate { };

    public RectTransform Rect { get; set; }

    public void OnSelect(BaseEventData eventData)
    {
        OnSelected.Invoke(this);
    }
}
