using System.Linq;
using UnityEngine;

public class ImageInventoryRepairer : MonoBehaviour
{
    [SerializeField] private ImageInventorySO _inventory;

    public void CheckAndRepairInventory()
    {
        if (IsInventoryIntact())
        {
            Debug.Log("Inventory is intact");
            return;
        }
        else
        {
            Debug.Log("Inventory is broken");
            RepairInventory();
        }
    }

    private bool IsInventoryIntact()
    {
        foreach (var item in _inventory.Images)
        {
            var temp = _inventory.AvailableImages.FirstOrDefault(n => n.name == item.Name);
            if (item.Level == null) return false;
            if (item.Level.GetInstanceID() != temp.GetInstanceID()) return false;
        }
        return true;
    }

    private void RepairInventory()
    {
        for (int i = 0; i < _inventory.Images.Count; i++)
        {
            UnlockableImageInventoryData item = _inventory.Images[i];
            _inventory.Images[i] = new UnlockableImageInventoryData(_inventory.AvailableImages.FirstOrDefault(n => n.name == item.Name), 
                                                                    item.IsUnlocked, item.Progress);
        }
    }
}