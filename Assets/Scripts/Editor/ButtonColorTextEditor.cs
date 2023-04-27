using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(ButtonColorText))]
public class ButtonColorTextEditor : ButtonEditor
{
    private SerializedProperty graphicElementsProperty;
    private SerializedProperty fadeDurationProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        graphicElementsProperty = serializedObject.FindProperty("colorTintGraphicElements");
        // fadeDurationProperty = serializedObject.FindProperty("graphicElementsFadeDuration");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.PropertyField(graphicElementsProperty);
        // EditorGUILayout.FloatField(fadeDurationProperty.floatValue);
        serializedObject.ApplyModifiedProperties();
    }
}
