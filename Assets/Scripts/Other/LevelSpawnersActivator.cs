using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnersActivator : MonoBehaviour
{
    [SerializeField] private LevelController _controller;

    private List<LevelSpawnerContainer> _spawners = new List<LevelSpawnerContainer>();
    private int _currentIndex = 0;

    public int CountLevels => _spawners.Count;

    private void Awake()
    {
        LevelSpawnerContainer[] containers = GetComponentsInChildren<LevelSpawnerContainer>();

        foreach (var container in containers)
            _spawners.Add(container);
    }

    private void OnEnable()
    {
        _controller.Started += OnLevelStarted;
    }

    private void OnDisable()
    {
        _controller.Started += OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        if (_currentIndex < _spawners.Count)
        {
            _spawners[_currentIndex].Init(_controller);
            _spawners[_currentIndex].Activate();
            _currentIndex++;
        }
    }
}
