using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnergyViewCoins : MonoBehaviour
{
    [SerializeField] private int _maxCount;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _slider;
    [SerializeField] private List<EnemySpawner> _enemySpawners;

    private int _currentCoin;

    public event UnityAction<int> CoinChanged;

    private void Awake()
    {
        _currentCoin = _maxCount;
    }

    private void OnEnable()
    {
        CoinChanged += OnCoinChanged;

        for (int i = 0; i < _enemySpawners.Count; i++)
            _enemySpawners[i].Spawned += OnEnemySpawned;
    }

    private void Start()
    {
        CoinChanged?.Invoke(_currentCoin);
    }

    private void OnDisable()
    {
        CoinChanged -= OnCoinChanged;

        for (int i = 0; i < _enemySpawners.Count; i++)
            _enemySpawners[i].Spawned -= OnEnemySpawned;
    }

    public bool CanBuy(int cost)
    {
        if (_currentCoin < cost)
            return false;

        _currentCoin -= cost;
        CoinChanged?.Invoke(_currentCoin);
        return true;
    }

    private void OnCoinChanged(int count)
    {
        _slider.value = count;
        _text.text = count.ToString() + $"/{_maxCount}";
    }

    private void OnEnemySpawned(EnemyStickman stickman)
    {
        Health health = stickman.GetComponent<Health>();
        health.Die += OnDie;
    }

    private void OnDie()
    {
        if (_currentCoin <= _maxCount)
        {
            _currentCoin++;
            CoinChanged?.Invoke(_currentCoin);
        }
    }
}