using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private SoundType soundType;

    private void Start()
    {
        button.onClick.AddListener(PlaySound);
    }

    private void PlaySound() => SoundManager.Instance.Play(soundType);

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
        if (soundType == SoundType.None)
        {
            Debug.Log($"SoundType is none for {this.gameObject.name}");
        }
    }

#endif
}
