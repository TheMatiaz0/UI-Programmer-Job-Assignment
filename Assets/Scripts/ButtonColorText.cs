using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonColorText : Button
{
    private TMP_Text text;

    protected override void Awake()
    {
        base.Awake();
        // TODO: Rewrite it with custom editor to SerializeField
        text = GetComponentInChildren<TMP_Text>();
    }

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
        if (base.gameObject.activeInHierarchy)
        {
            switch (this.transition)
            {
                case Selectable.Transition.ColorTint:
                    ColorTween(color * this.colors.colorMultiplier, instant);
                    break;
            }
        }
    }

    private void ColorTween(Color targetColor, bool instant)
    {
        base.image.CrossFadeColor(targetColor, (!instant) ? this.colors.fadeDuration : 0f, true, true);
        text.CrossFadeColor(targetColor.IsColorDark() ? Color.white : Color.black, (!instant) ? this.colors.fadeDuration : 0f, true, true);
    }
}