using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class Board : MonoBehaviour
{
    internal Action GameOver, GameWon;

    [SerializeField] private Queue _queue;

    private List<PuzzleTile> _tiles;

    public void Init(List<PuzzleTile> tiles)
    {
        _tiles = tiles;
        foreach (var tile in _tiles)
        {
            tile.Pressed += HandleTilePressed;
        }
        var tilesByCanvasOrder = _tiles.GroupBy(n => n.TilePresser.TileCanvas.sortingOrder).ToList();
        int indexOfCanvasOrder = 1;
        for (int i = 0; i < tilesByCanvasOrder.Count; i++)
        {
            var associatedKey = tilesByCanvasOrder.FirstOrDefault(n => n.Key == indexOfCanvasOrder);
            Debug.Log("Group " + associatedKey.Key + " has " + associatedKey.Count() + " elements");
            foreach (var tile in tilesByCanvasOrder[i])
            {
                tile.TilePresser.CheckIfCanBePressed();
            }
            indexOfCanvasOrder++;
        }

        _queue.QueueChanged += CheckIfTilesCanBePressed;
        _queue.TileReturned += CheckIfTilesCanBePressed;
        _queue.TileDestroyed += HandleTileDestroyed;
        _queue.GameOver += HandleGameOver;
    }

    private void HandleTileDestroyed(PuzzleTile tile)
    {
        _tiles.Remove(tile);
    }

    private void HandleGameOver()
    {
        GameOver?.Invoke();
    }

    private void HandleTilePressed(PuzzleTile tile)
    {
        _queue.AddToQueue(tile);
    }

    private void CheckIfTilesCanBePressed()
    {
        if (_tiles.Count == 0)
        {
            Debug.Log("GameWon");
            GameWon?.Invoke();
        }
        foreach (var tileFromList in _tiles)
        {
            tileFromList.CheckIfCanBePressed();
        }
    }

    public void ShuffleTiles()
    {
        var remainedTiles = _tiles.Where(n => n != null && n.IsInQueue == false).ToList();
        var remainingTilesType = remainedTiles.Select(n => n.TileData).ToList();
        foreach (var tile in remainedTiles)
        {
            var initialScale = transform.localScale;
            DOTween.Sequence().Append(tile.transform.DOScale(initialScale + new Vector3(0.2f, 0.2f, 0.2f), 0.4f))
                              .Append(tile.transform.DOScale(initialScale, 0.1f))
                              .AppendCallback(() =>
                              {
                                  var randomTileData = remainingTilesType[UnityEngine.Random.Range(0, remainingTilesType.Count)];
                                  tile.Init(randomTileData);
                                  remainingTilesType.Remove(randomTileData);
                              });
        }
    }

    public void AutomaticHelp()
    {
        if (_queue.Spots.Where(n => n.Tile != null).ToList().Count > 0)
        {
            Debug.Log("Automatic help when queue is not free");
            var tilesInQueue = _queue.Spots.Where(n => n.Tile != null).ToList();
            var tilesGroups = tilesInQueue.GroupBy(n => n.Tile.TileData).ToList();
            var biggestGroup = tilesGroups.FirstOrDefault(n => n.Count() == 2);
            foreach (var group in tilesGroups)
            {
                Debug.Log("Group " + group.Key + " has " + group.Count() + " elements");
            }
            if (biggestGroup == null)
            {
                biggestGroup = tilesGroups.First();
                var tilesToAdd = _tiles.Where(n => n != null && n.IsInQueue == false && n.TileData == biggestGroup.Key).Take(2).ToList();
                foreach (var tile in tilesToAdd)
                {
                    tile.PressOnTile();
                }
            }
            else if (biggestGroup.Count() == 2)
            {
                Debug.Log("Biggest Group " + biggestGroup.Key + " has " + biggestGroup.Count() + " elements");
                var tileToAdd = _tiles.First(n => n != null && n.IsInQueue == false && n.TileData == biggestGroup.Key);
                tileToAdd.PressOnTile();
            }
        }
        else
        {
            Debug.Log("Automatic help when queue is free");
            var randomTile = _tiles.Where(n => n != null).ToList().First();
            var threeTiles = _tiles.Where(n => n != null && n.TileData == randomTile.TileData).ToList().Take(3);
            foreach (var tile in threeTiles)
            {
                tile.PressOnTile();
            }
        }
    }

    public void Revive()
    {
        for (int i = 0; i < 5; i++)
        {
            if (_queue.CanCancelLastMove() == false)
                continue;
            _queue.CancelLastMove();
        }
        ShuffleTiles();
    }
}
