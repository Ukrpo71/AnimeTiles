using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryTip : MonoBehaviour
{
    [SerializeField] private ImageInventorySO _imageInventory;
    [SerializeField] private CanvasGroup _tip;

    private void OnEnable()
    {
        _imageInventory.InventoryChanged += UpdateAvailability;
        UpdateAvailability();
    }

    private void OnDisable()
    {
        _imageInventory.InventoryChanged -= UpdateAvailability;
    }

    public void UpdateAvailability()
    {
        if (PlayerPrefs.GetInt("GalleryTip") == 1)
        {
            if (_tip == null)
                _tip = GetComponent<CanvasGroup>();
            _tip.alpha = 1;
        }
        else
        {
            if (_tip == null)
                _tip = GetComponent<CanvasGroup>();
            _tip.alpha = 0;
        }
    }
}
