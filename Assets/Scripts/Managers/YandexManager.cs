using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YandexManager : PersistentSingleton<YandexManager>
{
    public Action YandexInitialized;
    public string Language { get; private set; } = "ru";

    private bool _gameReadyAlreadyCalled;

    private IEnumerator Start()
    {
        UnityEngine.PlayerPrefs.SetInt("AdsShowing", 0);
#if !UNITY_WEBGL || UNITY_EDITOR
        YandexInitialized?.Invoke();
        yield break;

#endif
        yield return YandexGamesSdk.Initialize(null);
        Language = YandexGamesSdk.Environment.i18n.tld;
        GameAnalyticsSDK.GameAnalytics.Initialize();
        YandexInitialized?.Invoke();
        //ShowInterstitialAd();
    }

    public void TryShowInterstitialAd()
    {
        //if (DataManager.Instance.SavedData.AdsTurnedOff)
        //    return;


#if UNITY_EDITOR
        Debug.Log("Showing interstitial in editor");
        return;
#endif
        
        InterstitialAd.Show(OnAdOpened, (data) => OnAdClosed(), (data) => OnAdClosed());
    }

    public void WatchRewardedVideo(Action action)
    {
        Debug.Log("Showing rewarded video");
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(OnAdOpened, action, OnAdClosed, (data) => OnAdClosed());
        return;
#endif

#if !UNITY_WEBGL || UNITY_EDITOR
        action.Invoke();
#endif
    }

    private void OnAdClosed()
    {
        UnityEngine.PlayerPrefs.SetInt("AdsShowing", 0);
        AudioListener.pause = false;
        AudioListener.volume = 1;
        Time.timeScale = 1;
    }

    private void OnAdOpened()
    {
        UnityEngine.PlayerPrefs.SetInt("AdsShowing", 1);
        AudioListener.pause = true;
        AudioListener.volume = 0;
        Time.timeScale = 0;
    }

    public void PurchaseConsumable(Action action, string yandexName)
    {
#if UNITY_EDITOR
        action.Invoke();
        return;
#endif
        OnAdOpened();
        Billing.PurchaseProduct(yandexName, (purchase) =>
        {
            Debug.Log($"Purchased {purchase.purchaseData.productID}");

            Billing.ConsumeProduct(purchase.purchaseData.purchaseToken, () =>
                {
                    action.Invoke();
                    OnAdClosed();
                }, (data) => 
                {
                    OnAdClosed();
                    Debug.LogError("Error trying to consume after purchasing " + yandexName + data);
                });

        }, (data) =>
        {
            OnAdClosed();
            Debug.LogError("Error trying to purchase " + yandexName + data);
        });
    }

    internal void InvokeGameReadyAPI()
    {
        if (_gameReadyAlreadyCalled) return;
#if UNITY_EDITOR
        Debug.Log("Invoking game ready API");
        return;
#endif
        YandexGamesSdk.GameReady();
        _gameReadyAlreadyCalled = true;
    }
}
