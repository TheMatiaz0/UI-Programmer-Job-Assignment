using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class NavigationElement
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private bool isAbleToPermanentSelect;
    [SerializeField]
    private UINavigationManager leadingPath;

    public Button Button => button;
    public bool IsAbleToPermanentSelect => isAbleToPermanentSelect;
    public UINavigationManager LeadingPath => leadingPath;
}

public class UINavigationManager : MonoBehaviour
{
    [SerializeField]
    private Navigation.Mode chosenMode;
    [SerializeField]
    private List<NavigationElement> elements;

    private Button currentLocked;
    private Button previousLocked;

    private GameObject lastSelected;

    public UINavigationManager Prior { get; private set; }

    private void OnEnable()
    {
        Setup();
    }

    private void Start()
    {

    }

    private void OnDisable()
    {
        // lastSelected = EventSystem.current.currentSelectedGameObject;
    }

    public void Setup()
    {
        foreach (var element in elements)
        {
            SetCurrentNavigationMode(element.Button, chosenMode);
            element.Button.onClick.AddListener(() => OnButtonClicked(element));
            var cancel = element.Button.AddComponent<CancelableSelectable>();
            cancel.OnCancelled += Cancel_OnCancelled;
        }
        EventSystem.current.SetSelectedGameObject(lastSelected ?? elements[0].Button.gameObject);
    }

    private void Cancel_OnCancelled(CancelableSelectable _)
    {
        Debug.Log("tso?");
        GoBack();
    }

    private void OnButtonClicked(NavigationElement clickedElement)
    {
        if (clickedElement.IsAbleToPermanentSelect)
        {
            previousLocked = currentLocked;
            if (previousLocked != null)
            {
                SetDominantColor(previousLocked, Color.white);
            }
            currentLocked = clickedElement.Button;
            GoTo(clickedElement.LeadingPath);
            SetDominantColor(currentLocked, Color.red);
        }
        else
        {
            previousLocked = currentLocked;
            if (previousLocked != null)
            {
                SetDominantColor(previousLocked, Color.white);
            }
            currentLocked = null;
        }
    }

    public void GoTo(UINavigationManager navigationPath)
    {
        if (navigationPath != null)
        {
            navigationPath.Prior = this;
            this.SetCurrentNavigationMode(Navigation.Mode.None);
            navigationPath.enabled = true;
            this.enabled = false;
        }
    }

    [ContextMenu("GO BACK")]
    public void GoBack()
    {
        Debug.Log($"return to {Prior}");
        GoTo(Prior);
    }

    public void SetCurrentNavigationMode(Navigation.Mode navigationMode)
    {
        foreach (var element in elements)
        {
            SetCurrentNavigationMode(element.Button, navigationMode);
        }
    }

    private void SetCurrentNavigationMode(Button button, Navigation.Mode navigationMode)
    {
        Navigation nav = button.navigation;
        nav.mode = navigationMode;
        button.navigation = nav;
    }

    private void SetDominantColor(Button btn, Color color)
    {
        btn.image.color = color;
        ColorBlock b = btn.colors;
        b.normalColor = color;
        btn.colors = b;
    }
}
