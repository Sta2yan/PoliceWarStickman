using UnityEngine;
using DG.Tweening;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private LevelComplete _levelComplete;
    [SerializeField] private float _distanceToMoveZ;
    [SerializeField] private float _time;

    private void OnEnable()
    {
        _levelComplete.Completed += OnComlete;
    }

    private void OnDisable()
    {
        _levelComplete.Completed -= OnComlete;
    }

    private void OnComlete(float delay)
    {
        Invoke(nameof(Move), delay);
    }

    private void Move()
    {
        transform.DOMove(new Vector3(transform.position.x, transform.position.y, transform.position.z + _distanceToMoveZ), _time);
    }

    private void OnValidate()
    {
        if (_time < 0)
            _time = 0;
    }
}
