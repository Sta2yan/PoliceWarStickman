using UnityEngine;
using DG.Tweening;

public class CameraMover : MonoBehaviour
{
    private const float StartDelay = 0.52f;

    [SerializeField] private float _distanceToMoveZ;
    [SerializeField] private float _time;
    [SerializeField] private LevelController _levelController;

    private void OnEnable()
    {
        _levelController.Ended += Move;
    }

    private void Start()
    {
        Invoke(nameof(Move), StartDelay);
    }

    private void OnDisable()
    {
        _levelController.Ended -= Move;
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
