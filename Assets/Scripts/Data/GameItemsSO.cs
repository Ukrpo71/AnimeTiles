using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameItems")]
public class GameItemsSO : ScriptableObject
{
    [field: SerializeField] public List<TipSO> Items;
}
