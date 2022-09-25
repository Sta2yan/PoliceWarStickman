using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnersActivator : MonoBehaviour
{
    [SerializeField] private List<LevelSpawnerContainer> _spawners;
    [SerializeField] private LevelController _controller;

    private int _currentIndex = 0;

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
