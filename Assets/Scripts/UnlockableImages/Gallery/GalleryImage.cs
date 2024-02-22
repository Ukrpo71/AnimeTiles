using UnityEngine;
using UnityEngine.UI;

public class GalleryImage : MonoBehaviour
{
    public UnlockableImageSO UnlockableImage => _unlockableImage;

    [SerializeField] private Image _fillImage;
    [SerializeField] private Button _button;
    [SerializeField] private ImageDialog _dialogPrefab;

    private UnlockableImageSO _unlockableImage;
    private float _filled;
    private Transform _canvasParent;



    public void SetImage(UnlockableImageInventoryData data, Transform canvasParent)
    {
        _unlockableImage = data.Level;
        _filled = data.Progress;
        _fillImage.sprite = data.Level.Image;
        _fillImage.fillAmount = _filled;
        _canvasParent = canvasParent;
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
        var dialog = Instantiate(_dialogPrefab, _canvasParent);
        dialog.Set(_unlockableImage, _filled);
        dialog.OnUnlock += UnlockImage;
    }

    private void UnlockImage(UnlockableImageSO unlockableImage)
    {
        _filled = 1;
        _fillImage.fillAmount = 1;
    }
}
