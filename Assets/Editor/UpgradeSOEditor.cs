using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*[CustomEditor(typeof(ItemSO))]
public class UpgradeSOEditor : Editor {
    private SerializedProperty upgradesProperty;

    private void OnEnable() {
        upgradesProperty = serializedObject.FindProperty("upgrades");
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        serializedObject.Update();
        EditorGUILayout.PropertyField(upgradesProperty, true);
        serializedObject.ApplyModifiedProperties();

    }
}*/