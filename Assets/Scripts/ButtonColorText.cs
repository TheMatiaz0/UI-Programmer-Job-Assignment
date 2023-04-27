using UnityEngine;
using UnityEngine.UI;

public class ButtonColorText : Button
{
    [SerializeField]
    private Graphic[] colorTintGraphicElements;

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        var color = state switch
        {
            Selectable.SelectionState.Normal => this.colors.normalColor,
            Selectable.SelectionState.Highlighted => this.colors.highlightedColor,
            Selectable.SelectionState.Pressed => this.colors.pressedColor,
            Selectable.SelectionState.Disabled => this.colors.disabledColor,
            SelectionState.Selected => this.colors.selectedColor,
            _ => Color.black,
        };
        if (base.gameObject.activeInHierarchy && this.transition == Selectable.Transition.ColorTint)
        {
            ColorTween(color * this.colors.colorMultiplier, instant);
        }
    }

    private void ColorTween(Color targetColor, bool instant)
    {
        base.image.CrossFadeColor(targetColor, (!instant) ? this.colors.fadeDuration : 0f, true, true);
        foreach (var graphic in colorTintGraphicElements)
        {
            graphic.CrossFadeColor(targetColor.IsColorDark() ? Color.white : Color.black, (!instant) ? this.colors.fadeDuration : 0f, true, true);
        }
    }
}