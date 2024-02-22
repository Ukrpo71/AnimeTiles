using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private DataInitiator _dataInitiator;
    [SerializeField] private DataController _dataController;
    [SerializeField] private BoardRoot _boardRoot;

    private void Start()
    {
        _dataInitiator.Init(DataManager.Instance.SavedData);
        _dataController.Init();
        _boardRoot.StartBoard();
        YandexManager.Instance.InvokeGameReadyAPI();
        YandexManager.Instance.TryShowInterstitialAd();
    }
}
