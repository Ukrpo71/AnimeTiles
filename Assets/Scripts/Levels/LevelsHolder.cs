using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsHolder : MonoBehaviour
{
    [SerializeField] private List<Level> _levels = new List<Level>();

    public Level GetLevelByIndex(int index)
    {
        if (index >= _levels.Count)
            return _levels[_levels.Count - 1];
        else
            return _levels[index];
    }
}
