using DG.Tweening;
using System;
using System.Collections.Generic;
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
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _helper;
    [SerializeField] private float _helperSpawnDelay;

    private List<GalleryImage> _images = new List<GalleryImage>();
    private Transform _parentToSpawnHelper;

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
        ClearImages();
        FillGalleryWithImage();
        _canvasGroup.DOFade(1, _timeToShow).OnComplete(() => _canvasGroup.blocksRaycasts = true);
    }

    private void CloseGallery()
    {
        _canvasGroup.DOFade(0, _timeToShow).OnComplete(() => _canvasGroup.blocksRaycasts = false);
    }

    private void FillGalleryWithImage()
    {
        for (int i = 0; i < _imageInventory.Images.Count; i++)
        {
            UnlockableImageInventoryData image = _imageInventory.Images[i];
            var galleryImage = Instantiate(_gallerImagePrefab, _parentForGallery);
            galleryImage.SetImage(image, _canvas.transform);
            if (i == 0)
            {
                _parentToSpawnHelper = galleryImage.transform;
                Invoke(nameof(SpawnHelper), _helperSpawnDelay);
            }
            _images.Add(galleryImage);
        }
    }

    private void ClearImages()
    {
        foreach(Transform child in _parentForGallery)
        {
            Destroy(child.gameObject);
        }
        _images.Clear();
    }

    private void SpawnHelper()
    {
        Instantiate(_helper, _parentToSpawnHelper);
    }

}
