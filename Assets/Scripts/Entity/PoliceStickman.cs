using UnityEngine;

[RequireComponent(typeof(StickmanMover), typeof(PoliceAttacker))]
public class PoliceStickman : MonoBehaviour
{
    [SerializeField] private float _radiusForFindTarget;

    private StickmanMover _mover;
    private PoliceAttacker _attacker;
    private Transform _targetToMove;

    public bool IsTargetFind => _targetToMove != null;

    private void Awake()
    {
        _mover = GetComponent<StickmanMover>();
        _attacker = GetComponent<PoliceAttacker>();
    }

    private void OnEnable()
    {
        _attacker.AttackStarted += _mover.Stop;
        _attacker.AttackEnded += _mover.Continue;
        _attacker.AttackEnded += OnEnded;
    }

    private void Update()
    {
        if (IsTargetFind)
            return;

        FindTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyStickman stickman))
        {
            _targetToMove = stickman.transform;
            _mover.SetTarget(_targetToMove);
        }
    }

    private void OnDisable()
    {
        _attacker.AttackStarted -= _mover.Stop;
        _attacker.AttackEnded -= _mover.Continue;
        _attacker.AttackEnded -= OnEnded;
    }

    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusForFindTarget);

        if (colliders != null)
        {
            Collider collider = colliders[Random.Range(0, colliders.Length)];

            if (collider.gameObject.TryGetComponent(out EnemyStickman stickman))
            {
                _targetToMove = stickman.transform;
                _mover.SetTarget(_targetToMove);
            }
        }
    }

    private void OnEnded()
    {
        _mover.ResetTarget();
        FindTarget();
    }

    private void OnValidate()
    {
        if (_radiusForFindTarget < 0)
            _radiusForFindTarget = 0;
    }
}
