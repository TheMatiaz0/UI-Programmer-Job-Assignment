using System;
using System.Collections;
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

public class SaveSlotsManager : MonoBehaviour
{
    [SerializeField]
    private List<Slot> slots;

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
        slotObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(slotObject);
    }
}
