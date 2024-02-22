using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadLevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private BoardRoot _boardRoot;

    private void OnEnable()
    {
        _button.onClick.AddListener(ReloadLevel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ReloadLevel);
    }

    private void ReloadLevel()
    {
        _boardRoot.ReloadLevel();
    }
}
