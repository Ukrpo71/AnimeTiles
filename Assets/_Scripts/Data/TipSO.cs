using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TipSO : ScriptableObject
{
    [field: SerializeField] public int Price { get; private set; }
}

[Serializable]
public struct InventoryItem
{
    public TipSO TipType;
    public int Quantity;
    public string Name;

    public InventoryItem(TipSO item, int quantity)
    {
        TipType = item;
        Quantity = quantity;
        Name = item.name;
    }

    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem
        {
            Quantity = newQuantity,
            TipType = this.TipType,
            Name = this.Name
        };
    }
}
