using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithInterstitialTrigger : MonoBehaviour
{

    private void OnEnable()
    {
        if (TryGetComponent(out Button button))
        {
            button.onClick.AddListener(ShowInter);
        }
    }

    private void OnDisable()
    {
        if (TryGetComponent(out Button button))
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void ShowInter()
    {
        YandexManager.Instance.TryShowInterstitialAd();
    }
}
