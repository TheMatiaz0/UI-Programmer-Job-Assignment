using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private Image image;

    public Image Image => image;


#if UNITY_EDITOR

    private void OnValidate()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

#endif
}
