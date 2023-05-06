using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button
{
    private const float GraphicElementsFadeDuration = 0.1f;

    [SerializeField]
    private Graphic[] colorTintGraphicElements;
    [Header("Only for non-Interactable Button")]
    [SerializeField]
    private bool extortSelection = false;

    public bool ExtortSelection => extortSelection;

    public override bool IsInteractable()
    {
        return base.IsInteractable() || extortSelection;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (interactable)
        {
            base.OnPointerClick(eventData);
        }
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        if (interactable)
        {
            base.OnSubmit(eventData);
        }
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        Color color;
        if (extortSelection)
        {
            color = GetDisabledColors(state);
        }
        else
        {
            color = GetNormalColor(state);
        }
        if (base.gameObject.activeInHierarchy && this.transition == Transition.ColorTint)
        {
            ColorTween(color * this.colors.colorMultiplier, instant);
        }
    }

    private Color GetNormalColor(SelectionState state)
    {
        return state switch
        {
            SelectionState.Normal => this.colors.normalColor,
            SelectionState.Highlighted => this.colors.highlightedColor,
            SelectionState.Pressed => this.colors.pressedColor,
            SelectionState.Disabled => this.colors.disabledColor,
            SelectionState.Selected => this.colors.selectedColor,
            _ => Color.black,
        };
    }

    private Color GetDisabledColors(SelectionState state)
    {
        return state switch
        {
            SelectionState.Normal => this.colors.disabledColor,
            SelectionState.Highlighted => BrightenColor(this.colors.disabledColor, 0.4f),
            SelectionState.Pressed => BrightenColor(this.colors.disabledColor, 0.2f),
            SelectionState.Disabled => this.colors.disabledColor,
            SelectionState.Selected => BrightenColor(this.colors.disabledColor, 0.4f),
            _ => Color.black,
        };
    }

    private Color BrightenColor(Color color, float summand)
    {
        Color.RGBToHSV(color, out var hue, out var saturation, out var brightness);
        brightness += summand;
        return Color.HSVToRGB(hue, saturation, brightness);
    }

    private void ColorTween(Color targetColor, bool instant)
    {
        base.image.CrossFadeColor(targetColor, (!instant) ? this.colors.fadeDuration : 0f, true, true);
        foreach (var graphic in colorTintGraphicElements)
        {
            graphic.CrossFadeColor(targetColor.IsColorDark() ? Color.white : Color.black, (!instant) ? GraphicElementsFadeDuration : 0f, true, true);
        }
    }
}