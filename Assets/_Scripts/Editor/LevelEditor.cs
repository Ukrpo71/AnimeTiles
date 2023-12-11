using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Level)), CanEditMultipleObjects]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Level level = (Level)target;
        if (GUILayout.Button("FindAllPuzzlePieces"))
        {
            FindPuzzlePieces(level);
        }
        if (GUILayout.Button("TurnOnAll"))
        {
            TurnEverythingOn(level.transform);
        }
        if (GUILayout.Button("TurnAllOff"))
        {
            TurnEverythingOff(level.transform);
        }

        base.OnInspectorGUI();
    }

    private void TurnEverythingOn(Transform transform)
    {
        foreach (Transform g in transform.GetComponentsInChildren<Transform>())
        {
            if (g.TryGetComponent(out ContentSizeFitter contentSizeFitter))
                contentSizeFitter.enabled = true;
            if (g.TryGetComponent(out HorizontalLayoutGroup horizontalGroup))
                horizontalGroup.enabled = true;
            if (g.TryGetComponent(out VerticalLayoutGroup verticalLayoutGroup))
                verticalLayoutGroup.enabled = true;

        }
    }

    private void TurnEverythingOff(Transform transform)
    {
        foreach (Transform g in transform.GetComponentsInChildren<Transform>())
        {
            if (g.TryGetComponent(out ContentSizeFitter contentSizeFitter))
                contentSizeFitter.enabled = false;
            if (g.TryGetComponent(out HorizontalLayoutGroup horizontalGroup))
                horizontalGroup.enabled = false;
            if (g.TryGetComponent(out VerticalLayoutGroup verticalLayoutGroup))
                verticalLayoutGroup.enabled = false;
        }
    }

    private void FindPuzzlePieces(Level level)
    {
        List<PuzzleTile> tiles = new List<PuzzleTile>();
        foreach (Transform g in level.transform.GetComponentsInChildren<Transform>())
        {
            if (g.TryGetComponent(out PuzzleTile tile))
                tiles.Add(tile);
        }
        level.SetTiles(tiles);
    }
}
