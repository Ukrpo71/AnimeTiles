using UnityEngine;
using UnityEngine.UI;

public class GalleryImage : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private Button _button;
    [SerializeField] private ImageDialog _dialogPrefab;

    private UnlockableImageSO _unlockableImage;
    private float _filled;

    public void SetImage(UnlockableImageInventoryData data)
    {
        _unlockableImage = data.Level;
        _filled = data.Progress;
        _fillImage.sprite = data.Level.Image;
        _fillImage.fillAmount = _filled;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenDialog);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenDialog);
    }

    private void OpenDialog()
    {
        var dialog = Instantiate(_dialogPrefab, transform);
        dialog.Set(_unlockableImage, _filled);
    }
}
