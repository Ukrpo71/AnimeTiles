
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TurnOffLayoutGroups : EditorWindow
{
    [MenuItem("Tools/Disable Horizontal Layout Groups")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(TurnOffLayoutGroups));
    }

    void OnGUI()
    {
        if (GUILayout.Button("Disable Layout Groups"))
        {
            DisableGroupsInPrefab(Selection.activeGameObject);
        }
    }

    void DisableGroupsInPrefab(GameObject prefab)
    {
        foreach (var @gameObject in PrefabUtility.GetAddedGameObjects(prefab))
        {
            var layoutGroup = @gameObject.instanceGameObject.GetComponent<HorizontalLayoutGroup>();
            if (layoutGroup != null)
            {
                layoutGroup.enabled = false;
            }
        }
    }
}
