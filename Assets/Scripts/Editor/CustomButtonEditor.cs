using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(CustomButton))]
public class CustomButtonEditor : ButtonEditor
{
    private SerializedProperty graphicElementsProperty;
    private SerializedProperty fullyDisabledProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        graphicElementsProperty = serializedObject.FindProperty("colorTintGraphicElements");
        fullyDisabledProperty = serializedObject.FindProperty("extortSelection");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.PropertyField(graphicElementsProperty);
        EditorGUILayout.PropertyField(fullyDisabledProperty);
        serializedObject.ApplyModifiedProperties();
    }
}
