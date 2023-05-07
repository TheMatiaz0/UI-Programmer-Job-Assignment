using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReverseUINavigationPointer : MonoBehaviour
{
    [SerializeField]
    private UINavigationManager previousNavigation;
    [SerializeField]
    private UINavigationManager currentNavigation;

    public void Setup(Selectable clicked)
    {
        var previousNav = previousNavigation.Elements.Find(x => x.LeadingPath == currentNavigation);
        if (currentNavigation.PreviousNavigation == null && previousNav != null)
        {
            previousNavigation.OnButtonClicked(previousNav);
            var currentNav = currentNavigation.Elements.Find(x => x.Selectable == clicked);
            currentNavigation.OnButtonClicked(currentNav);
            Debug.Log(currentNavigation.PreviousNavigation);
        }
    }
}
