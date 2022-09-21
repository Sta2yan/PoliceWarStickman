using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private float _timeToNextLevel;
    [SerializeField] private float _delay;
    [SerializeField] public UnityEvent<float> Complete;
    [SerializeField] public UnityEvent Start;

    private List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();

    public bool IsComplete { get; private set; }

    public event UnityAction<float> Completed
    {
        add => Complete.AddListener(value);
        remove => Complete.RemoveListener(value);
    }

    private void Awake()
    {
        EnemySpawner[] enemySpawners = FindObjectsOfType<EnemySpawner>();

        for (int i = 0; i < enemySpawners.Length; i++)
            _enemySpawners.Add(enemySpawners[i]);
    }

    private void Update()
    {
        if (IsComplete == false)
            if (Check() == true)
            {
                Complete?.Invoke(_delay);
                IsComplete = true;
                Invoke(nameof(NewLevel), _timeToNextLevel + _delay);
            }
    }

    private bool Check()
    {
        for (int i = 0; i < _enemySpawners.Count; i++)
            if (_enemySpawners[i].IsActive == true)
                return false;

        for (int i = 0; i < _enemySpawners.Count; i++)
            if (_enemySpawners[i].Stickmans.Count > 0)
                for (int j = 0; j < _enemySpawners[i].Stickmans.Count; j++)
                    if (_enemySpawners[i].Stickmans[j] != null)
                        return false;

        return true;
    }

    private void NewLevel()
    {
        Start?.Invoke();
    }
}
