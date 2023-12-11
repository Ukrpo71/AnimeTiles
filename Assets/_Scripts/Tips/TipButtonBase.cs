using UnityEngine;
using UnityEngine.UI;

public abstract class TipButtonBase : MonoBehaviour
{
    [SerializeField] protected Queue InGameQueue;

    [SerializeField] private Button _button;
    [SerializeField] private TipSO _tipData;
    [SerializeField] private InventorySO _inventory;
    [SerializeField] private Transform _purchaseParents;
    [SerializeField] private FloatVariable _money;
    [SerializeField] private Text _priceText;
    [SerializeField] private Text _quantityText;

    private bool _isBuying;

    protected abstract bool BoardConditionCheck();
    protected abstract void TipAction();

    private void OnEnable()
    {
        _button.onClick.AddListener(Click);
        InGameQueue.QueueChanged += UpdateAvailability;
        _inventory.InventoryChanged += UpdateAvailability;
        _money.OnValueChanged += UpdatePurchaseButtonAvailability;
        UpdateAvailability();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Click);
        InGameQueue.QueueChanged -= UpdateAvailability;
        _inventory.InventoryChanged -= UpdateAvailability;
        _money.OnValueChanged -= UpdatePurchaseButtonAvailability;
    }

    private void UpdateAvailability()
    {
        _isBuying = _inventory.ItemQuantityBiggerThanZero(_tipData) == false;
        _purchaseParents.gameObject.SetActive(_isBuying);
        if (_isBuying)
        {
            _button.interactable = _tipData.Price <= _money.Value;
            _priceText.text = _tipData.Price.ToString();
        }
        else
        {
            _button.interactable = BoardConditionCheck();
        }
        _quantityText.text = _inventory.GetItem(_tipData).Quantity.ToString();
    }

    private void UpdatePurchaseButtonAvailability(float money = 0)
    {
        if (_isBuying) _button.interactable = _tipData.Price <= _money.Value;
    }

    private void Click()
    {
        if (_isBuying)
        {
            PurchaseTip();
        }
        else
        {
            TipAction();
            _inventory.UpdateItemQuantity(_tipData, -1);
        }
    }

    private void PurchaseTip()
    {
        _inventory.UpdateItemQuantity(_tipData, 1);
        _money.Value -= _tipData.Price;
    }

    

}
