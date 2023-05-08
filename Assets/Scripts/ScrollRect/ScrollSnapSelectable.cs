using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollSnapSelectable : MonoBehaviour, ISelectHandler
{
    public event Action<ScrollSnapSelectable> OnSelected = delegate { };

    public RectTransform RectTransform { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnSelected.Invoke(this);
    }
}
