using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollSnapper : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;

    private void OnEnable()
    {
        foreach (Transform contentItem in scrollRect.content)
        {
            ScrollSnapSelectable selectable = contentItem.GetComponent<ScrollSnapSelectable>();
            if (selectable == null)
            {
                selectable = contentItem.AddComponent<ScrollSnapSelectable>();
            }
            selectable.OnSelected += OnSelected;
        }
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
        {
            SnapTo(EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>());
        }
    }

    private void OnDisable()
    {
        foreach (Transform item in scrollRect.content)
        {
            var selectable = item.GetComponent<ScrollSnapSelectable>();
            selectable.OnSelected -= OnSelected;
        }
    }

    private void OnSelected(ScrollSnapSelectable selectable)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(scrollRect.viewport, selectable.RectTransform.position))
        {
            SnapTo(selectable.RectTransform);
        }
    }

    public void SnapTo(RectTransform target)
    {
        scrollRect.content.localPosition = new(scrollRect.content.localPosition.x, scrollRect.SnapToChild(target).y);
    }
}
