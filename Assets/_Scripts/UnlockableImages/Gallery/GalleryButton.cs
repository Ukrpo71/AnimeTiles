﻿using UnityEngine;
using UnityEngine.UI;

public class GalleryButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Gallery _gallery;

    private void OnEnable()
    {
        _button.onClick.AddListener(ShowGallery);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ShowGallery);
    }

    private void ShowGallery()
    {
        _gallery.ShowGallery();
    }
}
