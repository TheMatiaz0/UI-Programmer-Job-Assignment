using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : UIBehaviour, IPointerClickHandler, ISelectHandler, IPointerEnterHandler, ISubmitHandler
{
    private const float HighlightCooldown = 0.3f;
    private const SoundType NotInteractableSelection = SoundType.SelectDisabled;

    [SerializeField]
    private SoundType pressedSound;
    [SerializeField]
    private SoundType highlightedSound;
    [SerializeField]
    private SoundType selectedSound;
    [SerializeField]
    private Selectable selectable;

    private static float lastHighlightTime;

    protected override void OnDisable()
    {
        base.OnDisable();
        lastHighlightTime = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Time.time >= lastHighlightTime && SoundManager.Instance != null)
        {
            lastHighlightTime = Time.time + HighlightCooldown;
            SoundManager.Instance.Play(selectable.interactable ? highlightedSound : NotInteractableSelection);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SoundManager.Instance != null && selectable.interactable)
        {
            SoundManager.Instance.Play(pressedSound);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (eventData is not PointerEventData && SoundManager.Instance != null)
        {
            SoundManager.Instance.Play(selectable.interactable ? selectedSound : NotInteractableSelection);
        }
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (SoundManager.Instance != null && selectable.interactable)
        {
            SoundManager.Instance.Play(pressedSound);
        }
    }

#if UNITY_EDITOR

    protected override void OnValidate()
    {
        base.OnValidate();
        if (selectable == null)
        {
            selectable = GetComponent<Selectable>();
        }
    }

#endif
}
