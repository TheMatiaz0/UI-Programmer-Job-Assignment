using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : UIBehaviour, IPointerClickHandler, ISelectHandler, IPointerEnterHandler, ISubmitHandler
{
    [SerializeField]
    private SoundType pressedSound;
    [SerializeField]
    private SoundType highlightedSound;
    [SerializeField]
    private SoundType selectedSound;
    [SerializeField]
    private float highlightCooldown = 1;

    private float lastHighlightCooldown;

    protected override void OnDisable()
    {
        base.OnDisable();
        lastHighlightCooldown = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Time.time >= lastHighlightCooldown)
        {
            lastHighlightCooldown = Time.time + highlightCooldown;
            SoundManager.Instance.Play(highlightedSound);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.Play(pressedSound);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (eventData is not PointerEventData)
        {
            SoundManager.Instance.Play(selectedSound);
        }
    }

    public void OnSubmit(BaseEventData eventData)
    {
        SoundManager.Instance.Play(pressedSound);
    }
}
