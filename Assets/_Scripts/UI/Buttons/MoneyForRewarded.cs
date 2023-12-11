using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyForRewarded : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private FloatVariable _money;
    [SerializeField] private float _amountToIncrease;

    private void OnEnable()
    {
        _button.onClick.AddListener(WatchRewarded);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(WatchRewarded);
    }

    private void WatchRewarded()
    {
        YandexManager.Instance.WatchRewardedVideo(() => _money.Value += _amountToIncrease);
    }
}
