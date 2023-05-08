using UnityEngine;
using UnityEngine.EventSystems;

public class CancelableSelectable : MonoBehaviour, ICancelHandler, ISelectHandler, IDeselectHandler, ISubmitHandler
{
    public void OnCancel(BaseEventData eventData)
    {
        ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.cancelHandler);
    }

    public void OnSelect(BaseEventData eventData)
    {
        ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.selectHandler);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.deselectHandler);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.submitHandler);
    }
}
