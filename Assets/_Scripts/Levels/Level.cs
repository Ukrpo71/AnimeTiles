using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<PuzzleTile> Tiles => _tilesOnBoard;

    private List<PuzzleTileSO> _possibleTiles => _tilesData.Tiles;
    [SerializeField] private List<PuzzleTile> _tilesOnBoard;
    [SerializeField] protected PossibleTiles _tilesData;

    public virtual void Init()
    {
        if (_tilesOnBoard.Count % 3 != 0)
        {
            Debug.LogError("Tiles are not equal to threes");
            while(_tilesOnBoard.Count% 3 != 0)
            {
                Destroy(_tilesOnBoard[_tilesOnBoard.Count - 1].gameObject);
                _tilesOnBoard.RemoveAt(_tilesOnBoard.Count - 1);
            }
        }
        List<PuzzleTile> tilesCopy = new List<PuzzleTile>();
        int possibleTilesIndex = 0;
        while (_tilesOnBoard.Where(n => n.TileData == null).ToList().Count > 0)
        {
            List<PuzzleTile> threeTiles = new List<PuzzleTile>();
            for (int i = 0; i < 3; i++)
            {
                threeTiles.Add(_tilesOnBoard[Random.Range(0, _tilesOnBoard.Count)]);
                tilesCopy.Add(threeTiles[i]);
                _tilesOnBoard.Remove(threeTiles[i]);
            }
            foreach (var tile in threeTiles)
            {
                tile.Init(_possibleTiles[possibleTilesIndex]);
            }
            possibleTilesIndex += 1;
            if (possibleTilesIndex >= _possibleTiles.Count)
                possibleTilesIndex = 0;
        }

        _tilesOnBoard = tilesCopy.ToList();
    }

    public void SetTiles(List<PuzzleTile> tiles)
    {
        _tilesOnBoard.Clear();
        _tilesOnBoard = tiles;
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssetIfDirty(this);
#endif
    }
}
