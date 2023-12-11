using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Board _board;
    [SerializeField] private LoseScreen _loseScreen;

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
        YandexManager.Instance.WatchRewardedVideo(Revive);
    }

    private void Revive()
    {
        _board.Revive();
        _loseScreen.Hide();
    }
}
