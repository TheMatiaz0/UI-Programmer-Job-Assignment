using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(SmoothScrollRect))]
public class SmoothScrollRectEditor : ScrollRectEditor
{
    private SerializedProperty shouldSmoothScrollProperty;
    private SerializedProperty smoothScrollTimeProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        shouldSmoothScrollProperty = serializedObject.FindProperty("shouldSmoothScroll");
        smoothScrollTimeProperty = serializedObject.FindProperty("smoothScrollTime");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(shouldSmoothScrollProperty);
        EditorGUILayout.PropertyField(smoothScrollTimeProperty);
        serializedObject.ApplyModifiedProperties();
    }
}
