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
    private Selectable selectable;
    [SerializeField]
    private bool isAbleToPermanentSelect;
    [SerializeField]
    private UINavigationManager leadingPath;

    public Selectable Selectable => selectable;
    public bool IsAbleToPermanentSelect => isAbleToPermanentSelect;
    public UINavigationManager LeadingPath => leadingPath;
}

public class UINavigationManager : MonoBehaviour
{
    [SerializeField]
    private Navigation.Mode chosenMode;
    [SerializeField]
    private List<NavigationElement> elements;

    private Selectable currentLocked;
    private Selectable previousLocked;

    private GameObject lastSelected;

    public UINavigationManager PreviousNavigation { get; private set; }

    private void OnEnable()
    {
        Setup();
    }

    private void OnDisable()
    {
        if (EventSystem.current != null)
        {
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void Setup()
    {
        foreach (var element in elements)
        {
            if (element.Selectable is Button)
            {
                var btn = element.Selectable as Button;
                btn.onClick.AddListener(() => OnButtonClicked(element));
            }
            SetCurrentNavigationMode(element.Selectable, chosenMode);
            var cancelSelectable = element.Selectable.AddComponent<CancelableSelectable>();
            cancelSelectable.OnCancelled += OnSelectableCancelled;
        }
        EventSystem.current.SetSelectedGameObject(lastSelected ?? elements[0]?.Selectable.gameObject);
    }

    private void OnSelectableCancelled(CancelableSelectable _)
    {
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
            currentLocked = clickedElement.Selectable;
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
            navigationPath.PreviousNavigation = this;
            this.SetCurrentNavigationMode(Navigation.Mode.None);
            navigationPath.enabled = true;
            this.enabled = false;
        }
    }

    public void GoBack()
    {
        GoTo(PreviousNavigation);
    }

    public void SetCurrentNavigationMode(Navigation.Mode navigationMode)
    {
        foreach (var element in elements)
        {
            SetCurrentNavigationMode(element.Selectable, navigationMode);
        }
    }

    private void SetCurrentNavigationMode(Selectable selectable, Navigation.Mode navigationMode)
    {
        Navigation navigation = selectable.navigation;
        navigation.mode = navigationMode;
        selectable.navigation = navigation;
    }

    private void SetDominantColor(Selectable selectable, Color color)
    {
        selectable.image.color = color;
        ColorBlock colors = selectable.colors;
        colors.normalColor = color;
        selectable.colors = colors;
    }
}
