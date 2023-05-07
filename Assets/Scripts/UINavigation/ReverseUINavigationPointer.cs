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
        var previousNavElement = previousNavigation.Elements.Find(x => x.LeadingPath == currentNavigation);
        if (currentNavigation.PreviousNavigation == null && previousNavElement != null)
        {
            previousNavigation.OnButtonClicked(previousNavElement);
            var currentNavElement = currentNavigation.Elements.Find(x => x.Selectable == clicked);
            currentNavigation.OnButtonClicked(currentNavElement);
        }
    }
}
