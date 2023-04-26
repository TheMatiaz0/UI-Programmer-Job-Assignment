using UnityEngine;
using UnityEngine.EventSystems;

public class PreventDeselection : MonoBehaviour
{
    private EventSystem eventSystem;
    private GameObject selection;

    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        if (eventSystem.currentSelectedGameObject != null && eventSystem.currentSelectedGameObject != selection)
        {
            selection = eventSystem.currentSelectedGameObject;
        }
        else if (selection != null && eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(selection);
        }
    }
}
