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

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.Play(pressedSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.Play(highlightedSound);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (eventData is PointerEventData)
        {
            return;
        }
        SoundManager.Instance.Play(selectedSound);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        SoundManager.Instance.Play(pressedSound);
    }
}