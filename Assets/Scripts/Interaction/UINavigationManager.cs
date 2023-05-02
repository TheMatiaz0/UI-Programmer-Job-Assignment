using System;
using System.Collections;
using System.Collections.Generic;
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

    private void OnEnable()
    {
        Setup();
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(lastSelected ?? elements[0].Button.gameObject);
    }

    private void OnDisable()
    {
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }

    public void Setup()
    {
        foreach (var element in elements)
        {
            SetCurrentNavigationMode(element.Button, chosenMode);
            element.Button.onClick.AddListener(() => OnButtonClicked(element));
        }
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
            if (clickedElement.LeadingPath != null)
            {
                SetCurrentNavigationMode(Navigation.Mode.None);
                clickedElement.LeadingPath.enabled = true;
            }
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

    private void SetDominantColor(Button btn, Color color)
    {
        btn.image.color = color;
        ColorBlock b = btn.colors;
        b.normalColor = color;
        btn.colors = b;
    }

    private void SetCurrentNavigationMode(Button button, Navigation.Mode mode)
    {
        Navigation nav = button.navigation;
        nav.mode = mode;
        button.navigation = nav;
    }

    private void SetCurrentNavigationMode(Navigation.Mode nav)
    {
        foreach (var item in elements)
        {
            SetCurrentNavigationMode(item.Button, nav);
        }
    }
}
