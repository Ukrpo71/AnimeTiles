using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleTilePresser : MonoBehaviour
{
    public Canvas TileCanvas => _canvas;

    [SerializeField] private Image _border;
    [SerializeField] private Image _icon;
    [SerializeField] private Color _enabledColor;
    [SerializeField] private Color _disabledColor;
    [SerializeField] private Button _button;
    [SerializeField] private Canvas _canvas;

    private Ray _ray;

    public void CheckIfCanBePressed()
    {
        if (_canvas.overrideSorting) return;

        _ray = new Ray(transform.position, transform.forward);
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(_ray.origin, _ray.direction);
        
        foreach (var hit in raycastHits)
        {
            if (hit.collider.TryGetComponent(out PuzzleTilePresser tilePresser))
            {
                if (tilePresser.TileCanvas.sortingOrder > _canvas.sortingOrder)
                {
                    ToggleButton(false);
                    return;
                }
            }
        }

        ToggleButton(true);
    }

    private void ToggleButton(bool toggle)
    {
        _button.enabled = toggle;
        _border.color = toggle ? _enabledColor : _disabledColor;
        if (_icon == null)
            _icon = transform.GetChild(0).GetComponent<Image>();
        _icon.color = toggle ? _enabledColor : _disabledColor;
    }
}
