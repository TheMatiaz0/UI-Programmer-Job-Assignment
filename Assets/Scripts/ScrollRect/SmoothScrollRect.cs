using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SmoothScrollRect : ScrollRect
{
    [SerializeField]
    private bool shouldSmoothScroll = true;
    [SerializeField]
    private float smoothScrollTime = 0.08f;

    public override void OnScroll(PointerEventData data)
    {
        if (!IsActive())
            return;

        if (shouldSmoothScroll)
        {
            Vector2 positionBefore = normalizedPosition;
            this.DOKill(complete: true);
            base.OnScroll(data);
            Vector2 positionAfter = normalizedPosition;

            normalizedPosition = positionBefore;
            this.DONormalizedPos(positionAfter, smoothScrollTime);
        }
        else
        {
            base.OnScroll(data);
        }
    }
}