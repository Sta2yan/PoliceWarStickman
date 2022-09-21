using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour, IDamagable
{
    [SerializeField] private float _timeToAttack;
    [SerializeField] private Bullet _template;
    [SerializeField] private Transform _point;
    [SerializeField] private float _radiusForFindTarget;

    private Health _target;

    public bool IsAttacking { get; private set; }
    public IReadOnlyNull Target => _target;
    
    public event UnityAction AttackStarted;
    public event UnityAction Attacked;
    public event UnityAction AttackEnded;

    private void Update()
    {
        if (_target != null)
            transform.LookAt(_target.transform);

        if (_target != null || IsAttacking)
            return;

        FindTarget();
    }

    public void Attack(Health health)
    {
        _target = health;
        IsAttacking = true;
        AttackStarted?.Invoke();

        StartAttack();
    }

    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusForFindTarget);

        if (colliders != null)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.TryGetComponent(out EnemyStickman stickman))
                {
                    if (stickman.TryGetComponent(out Health health))
                    {
                        Attack(health);
                        break;
                    }
                }
            }
        }
    }

    private void LeaveZone()
    {
        IsAttacking = false;
        AttackEnded?.Invoke();
    }

    private void StartAttack()
    {
        if (CanContinueAttack() == false)
            return;

        Bullet bullet = Instantiate(_template, _point);
        bullet.SetTarget(_target.transform);
        Attacked?.Invoke();

        if (CanContinueAttack() == false)
            return;

        Invoke(nameof(StartAttack), _timeToAttack);
    }

    private bool CanContinueAttack()
    {
        if (_target.IsAlive == false || IsAttacking == false)
        {
            IsAttacking = false;
            AttackEnded?.Invoke();
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnValidate()
    {
        if (_timeToAttack < 0)
            _timeToAttack = 0;

        if (_radiusForFindTarget < 0)
            _radiusForFindTarget = 0;
    }
}
