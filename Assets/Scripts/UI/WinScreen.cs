using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Text _levelFinishedText;
    [SerializeField] private Text _nextLevelText;
    [SerializeField] private ParticleSystem _attractorParticles;
    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private GameObject _ribbon;
    [SerializeField] private GameObject _coins;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private IntVariable _currentLevel;
    [SerializeField] private GameObject _background;

    public void Show()
    {
        _background.SetActive(true);
        DOTween.Sequence().AppendCallback(() => _nextLevelButton.gameObject.SetActive(true))
                          .AppendCallback(() => _levelFinishedText.gameObject.SetActive(true))
                          .AppendCallback(() => _attractorParticles.Play())
                          .AppendCallback(() => _coins.SetActive(true))
                          .AppendInterval(1f)
                          .AppendCallback(() => _explosionParticles.Play())
                          .Append(_nextLevelButton.transform.DOScale(0.8f, 0.5f).SetLoops(2, LoopType.Yoyo))
                          .AppendInterval(1f);
    }

    public void Hide()
    {
        _ribbon.gameObject.SetActive(false);
        _nextLevelButton.gameObject.SetActive(false);
        _levelFinishedText.gameObject.SetActive(false);
        _coins.SetActive(false);
        _background.SetActive(false);
    }


}
