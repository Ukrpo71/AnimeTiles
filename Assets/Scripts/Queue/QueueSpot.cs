using DG.Tweening;
using System;
using UnityEngine;

public class QueueSpot : MonoBehaviour
{
    public PuzzleTile Tile => _tile;
    public Action TileSet, TileReturned;
    public bool TileIsMoving { get; private set; }

    [SerializeField] private ParticleSystem _blueParticles;
    [SerializeField] private ParticleSystem _glowParticles;

    private PuzzleTile _tile;
    private Vector3 _tilePositionBeforeMovingToQueueSpot;
    private Transform _tileParentBeforeMovingToQueueSpot;

    public void SetTile(PuzzleTile tile)
    {
        _tile = tile;
        
        _tile.ToggleButton(false);
        MoveTile();
    }

    private void MoveTile()
    {
        TileIsMoving = true;
        _tile.ToggleCollider(false);
        _tile.ToggleCanvas(true);
        _tilePositionBeforeMovingToQueueSpot = _tile.transform.position;
        _tileParentBeforeMovingToQueueSpot = _tile.transform.parent;
        _tile.transform.SetParent(transform);
        _tile.transform.DOLocalMove(Vector3.zero, 0.5f).OnComplete(() =>
        {
            TileIsMoving = false;
            TileSet?.Invoke();
            _blueParticles.Play();
            _tile.ToggleCollider(false);
        });
        //Instantiate(_blueParticles, _rectTransform.position, Quaternion.identity);
        
    }

    internal void DestroyTile()
    {
        var previousTile = _tile;
        _tile = null;
        _glowParticles.Play();
        previousTile.transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.OutBounce).OnComplete(
            () =>
            {
                Destroy(previousTile.gameObject);
                
            });
        
    }

    public void FreeSpot()
    {
        _tile = null;
    }

    internal void ReturnToBoard()
    {
        _tile.transform.parent = _tileParentBeforeMovingToQueueSpot;
        _tile.ToggleCollider(true);
        _tile.ToggleCanvas(false);
        _tile.ToggleButton(true);
        _tile.transform.DOMove(_tilePositionBeforeMovingToQueueSpot, 0.5f).OnComplete(() => TileReturned?.Invoke());
        FreeSpot();
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        _glowParticles.Play();
    //    }
    //}
}
