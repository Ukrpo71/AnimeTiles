using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class UnlockableImagesTest : MonoBehaviour
{
    [SerializeField] private UnlockableImagePanel _panel;
    [SerializeField] private ImageInventorySO _imageInventory;

    [ContextMenu(nameof(Test))]
    public void Test()
    {
        _panel.Show();
        _panel.AddProgress(_imageInventory.GetNextImageToUnlock(), 0, 0.5f, () => _panel.Hide());
    }
}
