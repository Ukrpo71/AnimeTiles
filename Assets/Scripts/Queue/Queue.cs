using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Queue : MonoBehaviour
{
    public Action GameOver, QueueChanged, TileReturned;
    public Action<PuzzleTile> TileDestroyed;
    public List<QueueSpot> Spots => _spots;


    [SerializeField] private List<QueueSpot> _spots;
    [SerializeField] private AudioClip _match3sound;
    [SerializeField] private AudioClip _tilePressedSound;

    private void Start()
    {
        foreach (QueueSpot spot in _spots)
        {
            spot.TileSet += CheckIfMatched;
            spot.TileReturned += HandleTileReturned;
        }
    }

    private void HandleTileReturned()
    {
        TileReturned?.Invoke();
    }

    public void AddToQueue(PuzzleTile tile)
    {
        var freeSpots = _spots.Where(n => n.Tile == null).ToList();
        if (freeSpots.Count > 0)
        {
            freeSpots[0].SetTile(tile);
            SoundManager.Instance.PlaySound(_tilePressedSound);
            QueueChanged?.Invoke();
        }
    }

    private void CheckIfMatched()
    {
        var occupiedSpots = _spots.Where(n => n.Tile != null).ToList();
        for (int i = 0; i < occupiedSpots.Count; i++)
        {
            if (occupiedSpots[i].TileIsMoving) return;
        }
        for (int i = 0; i < occupiedSpots.Count; i++)
        {
            List<QueueSpot> sameTiles = new List<QueueSpot>();
            sameTiles.Add(_spots[i]);
            int countOfTheSameTiles = 0;
            for (int j = 0; j < occupiedSpots.Count; j++)
            {
                if (i == j)
                    continue;
                if (occupiedSpots[i].Tile.TileData == occupiedSpots[j].Tile.TileData)
                {
                    sameTiles.Add(occupiedSpots[j]);
                    countOfTheSameTiles++;
                }
            }
            if (sameTiles.Count >= 3)
            {
                SoundManager.Instance.PlaySound(_match3sound);
                for (int k = 0; k < 3; k++)
                {
                    TileDestroyed?.Invoke(sameTiles[k].Tile);
                    sameTiles[k].DestroyTile();
                }
                Invoke(nameof(RearangeQueue), 0.45f);
                return;
            }
        }
        if (occupiedSpots.Count == 7)
        {
            GameOver?.Invoke();
        }
    }

    internal void CancelLastMove()
    {
        var ocupiedSpots = _spots.Where(n => n.Tile != null).ToList();
        var lastOccupiedSpot = ocupiedSpots[ocupiedSpots.Count - 1];
        lastOccupiedSpot.ReturnToBoard();
        QueueChanged?.Invoke();
    }

    internal bool CanCancelLastMove()
    {
        return _spots.Where(n => n.Tile != null).ToList().Count > 0;
    }

    internal bool CanAutomaticHelp()
    {
        var occupiedSpots = _spots.Where(n => n.Tile != null);
        if (occupiedSpots.Count() <= 5)
            return true;
        else
            return occupiedSpots.GroupBy(n => n.Tile.TileData).ToList().FirstOrDefault(n => n.Count() == 2) != null;
    }

    private void RearangeQueue()
    {
        var stillOcupiedSpots = _spots.Where(n => n.Tile != null).ToList();
        if (stillOcupiedSpots.Count >= 0)
        {
            for (int i = 0; i < stillOcupiedSpots.Count; i++)
            {
                if (_spots[i].Tile != stillOcupiedSpots[i].Tile)
                {
                    _spots[i].SetTile(stillOcupiedSpots[i].Tile);
                    stillOcupiedSpots[i].FreeSpot();
                }
            }
        }
        QueueChanged?.Invoke();
    }

    internal void FreeQueue()
    {
        var stillOcupiedSpots = _spots.Where(n => n.Tile != null).ToList();
        foreach (var spot in stillOcupiedSpots)
        {
            Destroy(spot.Tile.gameObject);
            spot.FreeSpot();
        }
    }
}
