using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponSO))]
public class WeaponSOEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var script = (WeaponSO)target;

        if (GUILayout.Button("Generate Equipped SO", GUILayout.Height(40))) {
            EquippedSO equippedSO = WeaponManager.CreateEquippedSOFromWeaponSO(script);
            SaveEquippedSOToAssets(equippedSO);
        }

    }

    //https://discussions.unity.com/t/how-to-create-a-scriptableobject-file-with-specific-path-through-code/239303
    private void SaveEquippedSOToAssets(EquippedSO equippedSO) {
        string path = $"Assets/Scripts/ScriptableObjects/EquippedSO/{equippedSO.name}.asset";
        AssetDatabase.CreateAsset(equippedSO, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = equippedSO;
    }
}