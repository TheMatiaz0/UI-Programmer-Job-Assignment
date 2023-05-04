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
    [SerializeField]
    private Color lockedColor = Color.yellow;

    private Selectable currentSelectable;
    private Selectable previousSelectable;
    private Dictionary<Selectable, ColorBlock> selectableColorBlocks = new();
    private Dictionary<Selectable, Color> selectableImageColors = new();

    public UINavigationManager PreviousNavigation { get; private set; }

    private void OnEnable()
    {
        Setup();
    }

    private void OnDisable()
    {
        Debug.Log("oNdISABLE");
        // GoBack();
    }

    public void Setup()
    {
        selectableImageColors = new();
        selectableColorBlocks = new();
        foreach (var element in elements)
        {
            if (element.Selectable.image != null && element.Selectable.colors != null)
            {
                selectableImageColors.Add(element.Selectable, element.Selectable.image.color);
                selectableColorBlocks.Add(element.Selectable, element.Selectable.colors);
            }
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

        CleanLockState();

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
        PreviousNavigation?.CleanLockState();
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

    public void CleanLockState()
    {
        previousSelectable = currentSelectable;
        if (previousSelectable != null)
        {
            ClearLockedColor(previousSelectable);
        }
        currentSelectable = null;
    }

    private void SetLockState(NavigationElement clickedElement)
    {
        previousSelectable = currentSelectable;
        if (previousSelectable != null)
        {
            ClearLockedColor(previousSelectable);
        }
        currentSelectable = clickedElement.Selectable;
        GoTo(clickedElement.LeadingPath, false);
        SetLockedColor(currentSelectable, lockedColor);
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

    private void SetLockedColor(Selectable selectable, Color color)
    {
        selectable.image.color = color;
        ColorBlock colors = selectable.colors;
        colors.normalColor = color;
        selectable.colors = colors;
    }

    private void ClearLockedColor(Selectable selectable)
    {
        selectable.colors = selectableColorBlocks[selectable];
        selectable.image.color = selectableImageColors[selectable];
    }
}
