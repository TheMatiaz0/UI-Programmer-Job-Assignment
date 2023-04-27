using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(ButtonColorText))]
public class ButtonColorTextEditor : ButtonEditor
{
    private SerializedProperty graphicElementsProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        graphicElementsProperty = serializedObject.FindProperty("colorTintGraphicElements");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.PropertyField(graphicElementsProperty);
        serializedObject.ApplyModifiedProperties();
    }
}
