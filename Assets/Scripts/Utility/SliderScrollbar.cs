using UnityEngine;
using UnityEngine.UI;

public class SliderScrollbar : MonoBehaviour
{
    [SerializeField]
    private ScrollRect rect;
    [SerializeField]
    private Slider scrollSlider;

    private void OnEnable()
    {
        scrollSlider.onValueChanged.AddListener(UpdateScrollPosition);
        rect.onValueChanged.AddListener(UpdateSliderValue);
    }

    private void OnDisable()
    {
        scrollSlider.onValueChanged.RemoveListener(UpdateScrollPosition);
        rect.onValueChanged.RemoveListener(UpdateSliderValue);
    }

    private void UpdateScrollPosition(float value)
    {
        rect.verticalNormalizedPosition = 1 - value;
    }

    private void UpdateSliderValue(Vector2 scrollPosition)
    {
        scrollSlider.SetValueWithoutNotify(1 - scrollPosition.y);
    }
}