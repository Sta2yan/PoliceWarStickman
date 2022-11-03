using UnityEngine;
using DG.Tweening;

public class CameraMover : MonoBehaviour
{
    private const float StartDelay = 0.52f;

    [SerializeField] private float _distanceToMove;
    [SerializeField] private float _time;
    [SerializeField] private LevelController _levelController;

    private MoveAxis _axis = MoveAxis.Z;
    private Tween _tween;

    public float DefaultDistanceToMoveValue => _distanceToMove;

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

    public void Move()
    {
        switch (_axis)
        {
            case MoveAxis.X:
                _tween = transform.DOMove(new Vector3(transform.position.x + _distanceToMove, transform.position.y, transform.position.z), _time);
                break;
            case MoveAxis.Y:
                _tween = transform.DOMove(new Vector3(transform.position.x, transform.position.y + _distanceToMove, transform.position.z), _time);
                break;
            case MoveAxis.Z:
                _tween = transform.DOMove(new Vector3(transform.position.x, transform.position.y, transform.position.z + _distanceToMove), _time);
                break;
        }
    }

    public void SetMoveAxis(MoveAxis axis)
    {
        _axis = axis;
    }

    public void Stop()
    {
        if (_tween != null)
            _tween.Kill();
    }

    private void OnValidate()
    {
        if (_time < 0)
            _time = 0;
    }
}
