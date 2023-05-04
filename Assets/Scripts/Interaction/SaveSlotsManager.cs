using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class Slot
{
    [SerializeField]
    private Button slotButton;
    [SerializeField]
    private Button loadButton;

    public Button SlotButton => slotButton;
    public Button LoadButton => loadButton;
}

public class SaveSlotsManager : MonoBehaviour, ICancelHandler
{
    [SerializeField]
    private List<Slot> slots;

    private Button selectedSlotButton;

    private void Start()
    {
        foreach (var slot in slots)
        {
            slot.SlotButton.onClick.AddListener(() => OnSlotClicked(slot));
        }
    }

    private void OnSlotClicked(Slot clickedSlot)
    {
        foreach (var slot in slots)
        {
            if (slot != clickedSlot)
            {
                slot.LoadButton.gameObject.SetActive(false);
            }
        }
        var slotObject = clickedSlot.LoadButton.gameObject;
        selectedSlotButton = clickedSlot.SlotButton;
        slotObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(slotObject);
    }

    public void OnCancel(BaseEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(selectedSlotButton.gameObject);
    }
}
