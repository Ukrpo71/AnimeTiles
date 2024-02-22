using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Inventory")]
public class InventorySO : ScriptableObject
{
    public Action InventoryChanged;

    [field: SerializeField] public List<InventoryItem> Items;

    [SerializeField] private GameItemsSO _gameItems;

    public void InitializeWithStartingData()
    {
        Items.Clear();

        foreach (var item in _gameItems.Items)
        {
            Items.Add(new InventoryItem(item, 3));
        }
    }

    public InventoryItem GetItem(TipSO item)
    {
        var inventoryItem = Items.FirstOrDefault(n => n.Name == item.name);
        return inventoryItem;
    }

    public void UpdateItemQuantity(TipSO item, int increment)
    {
        var inventoryItem = Items.FirstOrDefault(n => n.Name == item.name);
        int index = Items.IndexOf(inventoryItem);
        Items[index] = Items[index].ChangeQuantity(inventoryItem.Quantity + increment);
        InventoryChanged?.Invoke();
    }

    public bool ItemQuantityBiggerThanZero(TipSO item) => Items.FirstOrDefault(n => n.TipType == item).Quantity > 0;

    public void LoadInventory(List<InventoryItem> items)
    {
        Items = items;
        if (IsInventoryIntact())
        {
            Debug.Log("Inventory is intact");
        }
        else
        {
            Debug.Log("Inventory is broken");
            RepairInventory();
        }
    }

    private bool IsInventoryIntact()
    {
        foreach (var item in Items)
        {
            var temp = _gameItems.Items.FirstOrDefault(n => n.name == item.Name);
            if (item.TipType == null) return false;
            if (item.TipType.GetInstanceID() != temp.GetInstanceID()) 
                return false;
        }
        return true;
    }

    private void RepairInventory()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItem item = Items[i];
            Items[i] = new InventoryItem(_gameItems.Items.FirstOrDefault(n => n.name == item.Name), item.Quantity);
        }
    }
}

