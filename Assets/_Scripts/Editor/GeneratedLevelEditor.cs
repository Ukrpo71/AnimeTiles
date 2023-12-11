using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GeneratedLevel)), CanEditMultipleObjects]
public class GeneratedLevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GeneratedLevel level = (GeneratedLevel)target;

        if (GUILayout.Button("GenerateLevel"))
        {
            level.Init();
            level.SetKeepGenerating(true);
        }
    }
}
