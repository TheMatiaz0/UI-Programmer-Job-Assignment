using DG.Tweening;
using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputFieldCodeTyper : UIBehaviour
{
    private delegate void SetAllowInputDelegate(TMP_InputField inputField, bool allowInput);
    private static readonly SetAllowInputDelegate SetAllowInputImpl = CreateSetAllowInputDelegate();

    private delegate void SetCaretVisibleDelegate(TMP_InputField inputField);
    private static readonly SetCaretVisibleDelegate SetCaretVisibleImpl = CreateSetCaretVisibleDelegate();

    private static FieldInfo m_AllowInputField;
    private static MethodInfo SetCaretVisibleMethod;

    [SerializeField]
    private TMP_InputField inputField;
    [TextArea(minLines: 9, maxLines: 19)]
    [SerializeField]
    private string resultText;
    [SerializeField]
    private TweenData animationData;

    private string cachedCode;

    protected override void Start()
    {
        base.Start();
        inputField.text = string.Empty;
        inputField.DOType(resultText, animationData.Duration)
            .SetEase(animationData.Ease)
            .SetLoops(-1, LoopType.Incremental)
            .OnUpdate(() => SetCaretVisible(inputField.text.Length));
    }

    private void SetCaretVisible(int pos)
    {
        inputField.caretPosition = pos;
        SetAllowInputImpl(inputField, true);
        SetCaretVisibleImpl(inputField);
    }

    private static SetAllowInputDelegate CreateSetAllowInputDelegate()
    {
        if (m_AllowInputField == null)
        {
            m_AllowInputField = typeof(TMP_InputField).GetField("m_AllowInput", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        return (inputField, allowInput) => m_AllowInputField.SetValue(inputField, allowInput);
    }

    private static SetCaretVisibleDelegate CreateSetCaretVisibleDelegate()
    {
        if (SetCaretVisibleMethod == null)
        {
            SetCaretVisibleMethod = typeof(TMP_InputField).GetMethod("SetCaretVisible", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        return (SetCaretVisibleDelegate)Delegate.CreateDelegate(typeof(SetCaretVisibleDelegate), SetCaretVisibleMethod);
    }


#if UNITY_EDITOR

    protected override void OnValidate()
    {
        base.OnValidate();
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
