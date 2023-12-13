using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{
    [SerializeField] private GalleryImage _gallerImagePrefab;
    [SerializeField] private Transform _parentForGallery;
    [SerializeField] private ImageInventorySO _imageInventory;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _timeToShow = 0.5f;
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(CloseGallery);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(CloseGallery);
    }

    public void ShowGallery()
    {
        _canvasGroup.DOFade(1, _timeToShow);
    }

    private void CloseGallery()
    {
        _canvasGroup.DOFade(0, _timeToShow);
    }

    private void Start()
    {
        FillGalleryWithImage();
    }

    private void FillGalleryWithImage()
    {
        foreach (var image in _imageInventory.Images)
        {
            var galleryImage = Instantiate(_gallerImagePrefab, _parentForGallery);
            galleryImage.SetImage(image);
        }
    }

}
