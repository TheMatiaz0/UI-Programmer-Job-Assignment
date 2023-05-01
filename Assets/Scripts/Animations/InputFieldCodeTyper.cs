using DG.Tweening;
using System.Reflection;
using TMPro;
using UnityEngine;

public class InputFieldCodeTyper : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    [TextArea(minLines: 9, maxLines: 19)]
    [SerializeField]
    private string resultText;
    [SerializeField]
    private TweenData animationData;

    private string cachedCode;

    private void Start()
    {
        inputField.text = string.Empty;
        inputField.DOType(resultText, animationData.Duration)
            .SetEase(animationData.Ease)
            .SetLoops(-1, LoopType.Incremental)
            .OnUpdate(() => SetCaretVisible(inputField.text.Length));
    }

    // TODO: Write custom caret logic, because using reflection in update is not a good performance idea.
    private void SetCaretVisible(int pos)
    {
        inputField.caretPosition = pos;
        inputField.GetType().GetField("m_AllowInput", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(inputField, true);
        inputField.GetType().InvokeMember("SetCaretVisible", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance, null, inputField, null);
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (inputField == null || string.IsNullOrEmpty(resultText))
        {
            return;
        }
        if (resultText != cachedCode)
        {
            inputField.SetTextWithoutNotify(resultText);
        }

        cachedCode = resultText;
    }

#endif
}
