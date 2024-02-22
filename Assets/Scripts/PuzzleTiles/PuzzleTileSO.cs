using UnityEngine;

[CreateAssetMenu(menuName = "PuzzleTile")]
public class PuzzleTileSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set;}
}
