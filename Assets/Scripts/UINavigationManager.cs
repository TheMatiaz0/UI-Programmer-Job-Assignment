using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UINavigationManager : MonoBehaviour
{
    [SerializeField]
    private Selectable[] selectable;

    [ContextMenu("Test")]
    public void Setup()
    {
        for (int i = 0; i < selectable.Length; i++)
        {
            Navigation navigation = new()
            {
                mode = Navigation.Mode.Explicit
            };
            if (i > 0)
            {
                navigation.selectOnUp = selectable[i - 1];
            }

            if (i < selectable.Length - 1)
            {
                navigation.selectOnDown = selectable[i + 1];
            }
            selectable[i].navigation = navigation;
        }
    }
}
