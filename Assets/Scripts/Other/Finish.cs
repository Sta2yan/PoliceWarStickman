using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private LevelController _controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyStickman enemy))
            _controller.LoseGame();
    }
}
