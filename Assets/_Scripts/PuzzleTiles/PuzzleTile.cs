using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleTile : MonoBehaviour
{
    public Action<PuzzleTile> Pressed;
    public PuzzleTileSO TileData => _tileSO;
    public PuzzleTilePresser TilePresser => _puzzleTilePresser;
    public bool IsInQueue { get; private set; } = false;

    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private PuzzleTilePresser _puzzleTilePresser;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Canvas _canvas;
    
    private PuzzleTileSO _tileSO;

    private void Awake()
    {
        _button.onClick.AddListener(PressOnTile);
    }

    public void Init(PuzzleTileSO tileSO)
    {
        _tileSO = tileSO;
        _image.sprite = _tileSO.Icon;
    }

    public void PressOnTile()
    {
        Pressed?.Invoke(this);
    }

    public void ToggleButton(bool value)
    {
        if (_button != null) _button.enabled = value;
    }

    public void ToggleCollider(bool value)
    {
        if (_collider != null)  _collider.enabled = value;
        IsInQueue = !value;
    }

    public void ToggleCanvas(bool value)
    {
        if (_canvas != null) _canvas.overrideSorting = value;
    }

    public void CheckIfCanBePressed()
    {
        if (_puzzleTilePresser != null) _puzzleTilePresser.CheckIfCanBePressed();
    }
}
