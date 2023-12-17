using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu]
public class ImageInventorySO : ScriptableObject
{
    public Action InventoryChanged;
    public List<UnlockableImageSO> AvailableImages => _availableImages;

    [field: SerializeField] public List<UnlockableImageInventoryData> Images;

    [SerializeField] private List<UnlockableImageSO> _availableImages;

    private ImageInventoryRepairer _inventoryRepairer = new ImageInventoryRepairer();

    public void InitializeWithStartingData()
    {
        Images.Clear();

        foreach (var item in _availableImages)
        {
            Images.Add(new UnlockableImageInventoryData(item, false, 0));
        }
    }

    public void LoadInventory(List<UnlockableImageInventoryData> images)
    {
        Images = images;
        _inventoryRepairer.CheckAndRepairInventory(this);
    }

    public bool DoesNextImageExists()
    {
        foreach (var item in Images)
        {
            if (item.Progress < 1f)
            {
                return true;
            }
        }
        return false;
    }

    public UnlockableImageInventoryData GetNextImageToUnlock()
    {
        return Images.FirstOrDefault(n => n.Progress < 1f);
    }

    public void UnlockImage(UnlockableImageSO item, bool saveAfter = true)
    {
        int index = Images.Select(n => n.Level).ToList().IndexOf(item);
        if (index >= 0)
        {
            Debug.Log("UnlockingItems");
            Images[index] = Images[index].UnlockItem();
            if (saveAfter) InventoryChanged?.Invoke();
        }
        else
        {
            Debug.Log("Item not found in inventory to unlock");
        }
    }

    public UnlockableImageInventoryData GetLevel(UnlockableImageSO item)
    {
        var inventoryItem = Images.FirstOrDefault(n => n.Name == item.name);
        return inventoryItem;
    }

    public void UpdateImageProgress(UnlockableImageInventoryData levelData, float increment)
    {
        int index = Images.IndexOf(levelData);
        Images[index] = Images[index].ChangeProgress(increment);
        if (Images[index].Progress == 1)
            PlayerPrefs.SetInt("GalleryTip", 1);
        InventoryChanged?.Invoke();
        
    }
}
