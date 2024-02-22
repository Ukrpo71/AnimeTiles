using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseInSizeAfterTime : MonoBehaviour
{
    [SerializeField] private float _timeToIncrease;
    [SerializeField] private float _timeToStart = 0;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        var lastScale = transform.localScale;
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        if (_canvasGroup) _canvasGroup.alpha = 0;
        transform.DOScale(lastScale, _timeToIncrease).SetDelay(_timeToStart);
        if (_canvasGroup) _canvasGroup.DOFade(1, _timeToIncrease).SetDelay(_timeToStart);
    }
}
