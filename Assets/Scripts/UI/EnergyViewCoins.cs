using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnergyViewCoins : MonoBehaviour
{
    [SerializeField] private int _maxCount;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _slider;
    [SerializeField] private LevelController _levelController;

    private EnemySpawner[] _enemySpawners;
    private int _currentCount;

    public int CurrentCount => _currentCount;

    public event UnityAction<int> CoinChanged;

    private void Awake()
    {
        _currentCount = _maxCount;
    }

    private void OnEnable()
    {
        CoinChanged += OnCoinChanged;
        _enemySpawners = FindObjectsOfType<EnemySpawner>();
        CoinChanged?.Invoke(_currentCount);

        if (_enemySpawners != null)
            foreach (var spawner in _enemySpawners)
                spawner.Spawned += OnEnemySpawned;

        _levelController.Ended += OnLevelEnd;
    }

    private void Start()
    {
        CoinChanged?.Invoke(_currentCount);
    }

    private void OnDisable()
    {
        CoinChanged -= OnCoinChanged;

        if (_enemySpawners != null)
            foreach (var spawner in _enemySpawners)
                spawner.Spawned -= OnEnemySpawned;

        _levelController.Ended -= OnLevelEnd;
    }

    public bool CanBuy(int cost)
    {
        if (_currentCount < cost)
            return false;

        _currentCount -= cost;
        CoinChanged?.Invoke(_currentCount);
        return true;
    }

    private void OnCoinChanged(int count)
    {
        _slider.value = count;
        _text.text = count.ToString() + $"/{_maxCount}";
    }

    private void OnEnemySpawned(EnemyStickman stickman)
    {
        stickman.EnergyCollected += OnEnergyCollected;
    }

    private void OnEnergyCollected(IEnergyCollectable stickman)
    {
        stickman.EnergyCollected -= OnEnergyCollected;
        _currentCount += stickman.EnergyCostBonus;

        if (_currentCount > _maxCount)
            _currentCount = _maxCount;

        CoinChanged?.Invoke(_currentCount);
    }

    private void OnLevelEnd()
    {
        _currentCount = _maxCount;
    }
}
