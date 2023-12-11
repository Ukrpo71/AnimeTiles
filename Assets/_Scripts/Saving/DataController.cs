using UnityEngine;

public class DataController : MonoBehaviour
{
    [SerializeField] private FloatVariable _money;
    [SerializeField] private BoolVariable _adsTurnedOff;
    [SerializeField] private IntVariable _currentLevel;
    [SerializeField] private InventorySO _inventory;
    [SerializeField] private DataManager _dataManager;

    public void Init()
    {
        _money.OnValueChanged += (obj) => SaveData();
        _adsTurnedOff.OnValueChanged += (obj) => SaveData();
        _currentLevel.OnValueChanged += (obj) => SaveData();
        _inventory.InventoryChanged += SaveData;
    }

    private void SaveData()
    {
        PlayerData dataToSave = new PlayerData(_currentLevel.Value, _adsTurnedOff.Value, _money.Value, _inventory.Items);
        DataManager.Instance.SaveData(dataToSave);
    }
}
