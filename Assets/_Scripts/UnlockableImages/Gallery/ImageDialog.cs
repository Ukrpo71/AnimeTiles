using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageDialog : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _rewardedButton;
    [SerializeField] private ImageInventorySO _imageInventorySO;

    private UnlockableImageSO _imageToUnlock;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(CloseDialog);
        _rewardedButton.onClick.AddListener(HandleRewardedClicked);
    }

    private void HandleRewardedClicked()
    {
        _imageInventorySO.UnlockImage(_imageToUnlock);
        _image.fillAmount = 1;
        _rewardedButton.gameObject.SetActive(false);
    }

    private void CloseDialog()
    {
        Destroy(gameObject);
    }

    public void Set(UnlockableImageSO image, float fillAmount)
    {
        _imageToUnlock = image;
        _image.sprite = image.Image;
        _image.fillAmount = fillAmount;
        _rewardedButton.gameObject.SetActive(fillAmount == 1);
    }
}
