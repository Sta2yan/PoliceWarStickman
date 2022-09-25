using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyStickman _template;
    [SerializeField] private Transform _targetToMove;
    [SerializeField] private int _count;
    [SerializeField] private float _time;
    [SerializeField] private float _timeToStartSpawn;
    [SerializeField] private UnityEvent _started;
    [SerializeField] private LevelController _levelController;

    private WaitForSeconds _sleep;
    private List<EnemyStickman> _stickmans = new List<EnemyStickman>();
    private float _currentTime;
    private bool _isUsed;

    public bool IsActive { get; private set; }
    public bool IsUsed => _isUsed;
    public IReadOnlyList<EnemyStickman> Stickmans => _stickmans;

    public event UnityAction<EnemyStickman> Spawned; 

    private void Awake()
    {
        _sleep = new WaitForSeconds(_time);
        IsActive = true;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isUsed == false)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _timeToStartSpawn)
            {
                StartCoroutine(StartSpawn());
                _isUsed = true;
                _started?.Invoke();
            }
        }
    }

    private IEnumerator StartSpawn()
    {
        for (int i = 0; i < _count; i++)
        {
            if (_levelController.IsLose)
                break;

            EnemyStickman enemy = Instantiate(_template, transform);
            _stickmans.Add(enemy);

            if (enemy.TryGetComponent(out AttackerAnim anim))
                anim.Init(_levelController as IOnlyActionLose);

            enemy.SetTarget(_targetToMove);
            Spawned?.Invoke(enemy);

            if (i == _count - 1)
                IsActive = false;

            yield return _sleep;
        }
    }
}
