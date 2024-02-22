using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PossibleTiles")]
public class PossibleTiles : ScriptableObject
{
    [field: SerializeField] public List<PuzzleTileSO> Tiles { get; private set; }
}
