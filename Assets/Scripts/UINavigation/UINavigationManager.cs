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

public class UINavigationManager : MonoBehaviour, ICancelHandler, ISelectHandler
{
    public event Action<UINavigationManager> OnWentBack = delegate { };

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
    private GameObject lastSelected;

    public UINavigationManager PreviousNavigation { get; private set; }

    private void OnEnable()
    {
        Setup();
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

        CleanLockedState();
        Reselect();
    }

    private void Reselect()
    {
        if (elements.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected == null ? elements[0].Selectable.gameObject : lastSelected);
        }
    }

    public void OnCancel(BaseEventData eventData)
    {
        GoBack();
    }

    public void GoBack(bool invokeCallback = true)
    {
        if (invokeCallback)
        {
            OnWentBack(this);
        }
        PreviousNavigation?.CleanLockedState();
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
            SetLockedState(clickedElement);
        }
        else
        {
            CleanLockedState();
        }
    }

    public void CleanLockedState()
    {
        SetPreviousSelectable();
        currentSelectable = null;
    }

    private void SetLockedState(NavigationElement clickedElement)
    {
        SetPreviousSelectable();
        currentSelectable = clickedElement.Selectable;
        GoTo(clickedElement.LeadingPath, false);
        SetLockedColor(currentSelectable, lockedColor);
    }

    private void SetPreviousSelectable()
    {
        previousSelectable = currentSelectable;
        if (previousSelectable != null)
        {
            ClearLockedColor(previousSelectable);
        }
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

    public void OnSelect(BaseEventData eventData)
    {
        var navigationElement = elements.Find(x => x.Selectable.gameObject == eventData.selectedObject);
        if (navigationElement != null)
        {
            lastSelected = navigationElement.Selectable.gameObject;
        }
    }
}
