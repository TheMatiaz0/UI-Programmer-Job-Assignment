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

public class UINavigationManager : MonoBehaviour, ICancelHandler
{
    public event Action OnWentBack = delegate { };

    [SerializeField]
    private Navigation.Mode chosenMode;
    [SerializeField]
    private List<NavigationElement> elements;

    private Selectable currentLocked;
    private Selectable previousLocked;

    public UINavigationManager PreviousNavigation { get; private set; }

    private void OnEnable()
    {
        Setup();
    }

    public void Setup()
    {
        CleanLockState();
        foreach (var element in elements)
        {
            if (element.Selectable is Button)
            {
                var button = element.Selectable as Button;
                button.onClick.AddListener(() => OnButtonClicked(element));
            }
            SetCurrentNavigationMode(element.Selectable, chosenMode);
            var cancelSelectable = element.Selectable.GetComponent<CancelableSelectable>();
            if (cancelSelectable == null)
            {
                cancelSelectable = element.Selectable.AddComponent<CancelableSelectable>();
            }
        }
        if (elements.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(elements[0].Selectable.gameObject);
        }
    }

    public void OnCancel(BaseEventData eventData)
    {
        GoBack();
    }

    public void GoBack()
    {
        OnWentBack();
        GoTo(PreviousNavigation, true);
    }

    public void GoTo(UINavigationManager navigationPath, bool isGoingBack)
    {
        if (navigationPath != null)
        {
            if (!isGoingBack)
            {
                navigationPath.PreviousNavigation = this;
            }
            this.SetCurrentNavigationMode(Navigation.Mode.None);
            navigationPath.enabled = true;
            this.enabled = false;
        }
    }

    private void OnButtonClicked(NavigationElement clickedElement)
    {
        if (clickedElement.IsAbleToPermanentSelect)
        {
            SetLockState(clickedElement);
        }
        else
        {
            CleanLockState();
        }
    }

    private void CleanLockState()
    {
        previousLocked = currentLocked;
        if (previousLocked != null)
        {
            SetDominantColor(previousLocked, Color.white);
        }
        currentLocked = null;
    }

    private void SetLockState(NavigationElement clickedElement)
    {
        previousLocked = currentLocked;
        if (previousLocked != null)
        {
            SetDominantColor(previousLocked, Color.white);
        }
        currentLocked = clickedElement.Selectable;
        GoTo(clickedElement.LeadingPath, false);
        SetDominantColor(currentLocked, Color.red);
    }

    private void SetCurrentNavigationMode(Navigation.Mode navigationMode)
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
