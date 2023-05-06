using UnityEngine;
using UnityEngine.EventSystems;

public class CancelableSelectable : MonoBehaviour, ICancelHandler, ISelectHandler
{
    public void OnCancel(BaseEventData eventData)
    {
        ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.cancelHandler);
    }

    public void OnSelect(BaseEventData eventData)
    {
        ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.selectHandler);
    }
}
