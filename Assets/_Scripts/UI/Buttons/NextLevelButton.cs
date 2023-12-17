using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private BoardRoot _boardRoot;
    [SerializeField] private UnlockableImagePanel _panel;
    [SerializeField] private ImageInventorySO _imageInventory;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        _button?.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        if (_imageInventory.DoesNextImageExists())
        {
            UnlockableImageInventoryData imageData = _imageInventory.GetNextImageToUnlock();
            _panel.Show();
            _panel.AddProgress(imageData, imageData.Progress, imageData.Progress + imageData.Level.AmountToIncreaseProgress, () => StartCoroutine(ShowImageCoroutine()));
            _imageInventory.UpdateImageProgress(imageData, imageData.Level.AmountToIncreaseProgress);
        }
        else
        {
            _boardRoot.LoadNextLevel();
        }
    }

    private IEnumerator ShowImageCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        _panel.Hide();
        _boardRoot.LoadNextLevel();
    }
}
