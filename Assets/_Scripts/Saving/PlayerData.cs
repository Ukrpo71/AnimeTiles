using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct PlayerData
{
    public int Level; 
    public bool AdsTurnedOff; 
    public float Money; 
    public List<InventoryItem> TipsData;

    public PlayerData(int level, bool adsTurnedOff, float money, List<InventoryItem> tipsData)
    {
        Level = level;
        AdsTurnedOff = adsTurnedOff;
        Money = money;
        TipsData = tipsData;
    }
}
