using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollSnapSelectable : MonoBehaviour, ISelectHandler
{
    public event Action<ScrollSnapSelectable> OnSelected = delegate { };

    public RectTransform Rect { get; private set; }

    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnSelected.Invoke(this);
    }
}
