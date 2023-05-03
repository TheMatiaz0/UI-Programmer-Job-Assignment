using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSnapper : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;

    private void OnEnable()
    {
        foreach (Transform contentItem in scrollRect.content)
        {
            var selectable = contentItem.AddComponent<ScrollSnapSelectable>();
            selectable.Rect = contentItem.GetComponent<RectTransform>();
            selectable.OnSelected += OnSelected;
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
        SnapTo(selectable.Rect);
    }

    public void SnapTo(RectTransform target)
    {
        scrollRect.content.localPosition = new(scrollRect.content.localPosition.x, scrollRect.SnapToChild(target).y);
    }
}
