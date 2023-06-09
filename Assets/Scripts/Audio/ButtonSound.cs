using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : UIBehaviour, IPointerClickHandler, ISelectHandler, IPointerEnterHandler, ISubmitHandler
{
    private const SoundType NotInteractableSelection = SoundType.SelectDisabled;
    private const SoundType NotInteractableClick = SoundType.ClickDisabled;

    [SerializeField]
    private SoundType pressedSound;
    [SerializeField]
    private SoundType highlightedSound;
    [SerializeField]
    private SoundType selectedSound;
    [SerializeField]
    private Selectable selectable;
    [SerializeField]
    private float highlightCooldown = 0.2f;

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
            lastHighlightTime = Time.time + highlightCooldown;
            SoundManager.Instance.Play(selectable.interactable ? highlightedSound : NotInteractableSelection);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play(selectable.interactable ? pressedSound : NotInteractableClick);
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
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play(selectable.interactable ? pressedSound : NotInteractableClick);
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
