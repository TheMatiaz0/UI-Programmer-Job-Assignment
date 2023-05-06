using UnityEngine;
using UnityEngine.EventSystems;

public class CancelableSelectable : MonoBehaviour, ICancelHandler
{
    public void OnCancel(BaseEventData eventData)
    {
        ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.cancelHandler);
    }
}
