using TMPro;
using UnityEngine;

public class VersionNumber : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    private void Start()
    {
        text.SetText(GetVersion());
    }

    private void OnValidate()
    {
        var newVersion = GetVersion();
        if (text.text != newVersion)
        {
            text.SetText(GetVersion());
        }
    }

    private string GetVersion()
    {
        return $"{Application.version} ({Application.unityVersion})";
    }
}
