using System;
using UnityEngine;

public class DataInitiator : MonoBehaviour
{
    [SerializeField] private FloatVariable _money;
    [SerializeField] private BoolVariable _adsTurnedOff;
    [SerializeField] private IntVariable _currentLevel;
    [SerializeField] private InventorySO _inventory;
    [SerializeField] private ImageInventorySO _imageInventory;
    public void Init(PlayerData savedData)
    {
        _money.Value = savedData.Money;
        _inventory.LoadInventory(savedData.TipsData);
        _imageInventory.LoadInventory(savedData.ImagesData);
        _adsTurnedOff.Value = savedData.AdsTurnedOff;
        _currentLevel.Value = savedData.Level;
    }
}
