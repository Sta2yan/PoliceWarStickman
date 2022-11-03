using System.Collections.Generic;
using UnityEngine;

public class WarningZoneForSpawn : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _warnings;
    [SerializeField] private LevelController _levelController;
    [SerializeField] private int _level;
    [SerializeField] private float _timeToDelete;

    private float _currentTime = 0f;
    private bool _isPlay = false;

    private void OnEnable()
    {
        _levelController.Changed += OnLevelChanged;

        for (int i = 0; i < _warnings.Count; i++)
            _warnings[i].gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isPlay)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _timeToDelete)
                for (int i = 0; i < _warnings.Count; i++)
                    _warnings[i].gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _levelController.Changed -= OnLevelChanged;
    }

    private void OnLevelChanged(int level)
    {
        if (_level == level)
        {
            for (int i = 0; i < _warnings.Count; i++)
                _warnings[i].gameObject.SetActive(true);

            _isPlay = true;
        }
    }

    private void OnValidate()
    {
        if (_timeToDelete < 0f)
            _timeToDelete = 0f;
    }
}
