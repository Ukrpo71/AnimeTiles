using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _coinDestination;
    [SerializeField] private Transform _coinsParent;

    private List<GameObject> _coins = new List<GameObject>();

    private void OnEnable()
    {
        StartCoroutine(AnimateCoins());
    }

    private void OnDisable()
    {
        foreach (var coin in _coins)
        {
            if (coin.gameObject)
                Destroy(coin.gameObject);
        }
        _coins.Clear();
    }

    private IEnumerator AnimateCoins()
    {
        for (int i = 0; i < 5; i++)
        {
            var coin = Instantiate(_coinPrefab, _coinsParent);
            _coins.Add(coin);
            coin.transform.DOMove(_coinDestination.transform.position, 0.5f).OnComplete(() => Destroy(coin.gameObject));
            yield return new WaitForSeconds(0.2f);
        }
    }
}
