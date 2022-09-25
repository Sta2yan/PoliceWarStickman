using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnerContainer : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _spawners;

    private LevelController _controller;
    private bool _isComplete;

    private void Update()
    {
        if (_isComplete == false)
            CheckWavesEnd();
    }

    public void Init(LevelController controller)
    {
        if (controller != null)
            _controller = controller;
    }

    public void Activate()
    {
        for (int i = 0; i < _spawners.Count; i++)
            _spawners[i].gameObject.SetActive(true);

        _isComplete = false;
    }

    private void CheckWavesEnd()
    {
        for (int i = 0; i < _spawners.Count; i++)
            if (_spawners[i].IsUsed == false || _spawners[i].IsActive == true)
                return;

        EnemyAttacker[] attackers = FindObjectsOfType<EnemyAttacker>();

        if (attackers.Length > 0)
            return;

        if (_controller != null)
            _controller.StartEndGame();

        _isComplete = true;
    }
}
