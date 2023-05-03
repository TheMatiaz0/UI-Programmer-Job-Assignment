using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CancelableSelectable : MonoBehaviour, ICancelHandler
{
    public event Action<CancelableSelectable> OnCancelled = delegate { };

    public void OnCancel(BaseEventData eventData)
    {
        ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.cancelHandler);
        OnCancelled.Invoke(this);
    }
}
