using System.Linq;
using UnityEngine;

public class ImageInventoryRepairer
{
    public void CheckAndRepairInventory(ImageInventorySO inventory)
    {
        if (IsInventoryIntact(inventory))
        {
            Debug.Log("Image inventory is intact");
            return;
        }
        else
        {
            Debug.Log("Image inventory is broken");
            RepairInventory(inventory);
        }
    }

    private bool IsInventoryIntact(ImageInventorySO inventory)
    {
        foreach (var item in inventory.Images)
        {
            var temp = inventory.AvailableImages.FirstOrDefault(n => n.name == item.Name);
            if (item.Level == null) return false;
            if (item.Level.GetInstanceID() != temp.GetInstanceID()) return false;
        }
        return true;
    }

    private void RepairInventory(ImageInventorySO inventory)
    {
        for (int i = 0; i < inventory.Images.Count; i++)
        {
            UnlockableImageInventoryData item = inventory.Images[i];
            inventory.Images[i] = new UnlockableImageInventoryData(inventory.AvailableImages.FirstOrDefault(n => n.name == item.Name), 
                                                                    item.IsUnlocked, item.Progress);
        }
    }
}