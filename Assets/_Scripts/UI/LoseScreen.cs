using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private GameObject _popUp;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _darkUnderlay;
    public void Show()
    {
        _popUp.gameObject.SetActive(true);
        _darkUnderlay.gameObject.SetActive(true);
        var previousScale = _popUp.transform.localScale;
        _popUp.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        DOTween.Sequence().Append(_popUp.transform.DOScale(previousScale.x, 1f))
                          .Join(_canvasGroup.DOFade(1, 1f));
    }

    public void Hide()
    {
        DOTween.Sequence().Append(_canvasGroup.DOFade(0, 1f))
                          .AppendCallback(() => _popUp.gameObject.SetActive(false))
                          .AppendCallback(() => _darkUnderlay.gameObject.SetActive(false));
    }
}
